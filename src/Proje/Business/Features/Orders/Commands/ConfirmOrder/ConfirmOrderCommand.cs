using AutoMapper;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Business.Services.PurseService;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Commands.ConfirmOrder
{
    public class ConfirmOrderCommand:IRequest<ConfirmOrderDto>,ISecuredRequest
    {
        public int OrderId { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, ConfirmOrderDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPurseService _purseService;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public ConfirmOrderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, 
                IPurseService purseService, OrderBusinessRules orderBusinessRules, 
                OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _purseService = purseService;
                _orderBusinessRules = orderBusinessRules;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<ConfirmOrderDto> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
            {
                Order order = await _orderBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.OrderId);
                await _orderBusinessRules.OrderStatusMustBeFalse(order.Status);
                await _orderDetailBusinessRules.IsThereAnyProductInTheCart(order.Id);

                UserCart? userCart = await _unitOfWork.UserCartDal.GetAsync(u => u.Id == order.UserCartId);
                Purse? purse = await _unitOfWork.PurseDal.GetAsync(p => p.UserId == userCart.UserId);
                
                float totalPrice = 0;

                IPaginate<OrderDetail> orderDetails = await _unitOfWork.OrderDetailDal.GetListAsync(
                    o => o.OrderId == request.OrderId,
                    include: c => c.Include(c => c.Product)
                );
                List<Product> products = new List<Product>();
                foreach (var item in orderDetails.Items)  //sepetin tutarı hesaplanır
                {
                    totalPrice += item.TotalPrice;
                    products.Add(item.Product);
                }
                foreach (var item in orderDetails.Items) //stoktaki ürünlerin miktarı azaltılır
                {
                    foreach (var product in products)
                    {
                        if (product.Id == item.ProductId)
                        {
                            product.Quantity -= item.Quantity;
                        }
                    }
                }
                _unitOfWork.ProductDal.UpdateRange(products); //alınan ürünler toplu bir şekilde güncellenir

                await _purseService.SpendMoney(purse,totalPrice);

                order.Status = true;
                order.ApprovalDate = Convert.ToDateTime(DateTime.Now.ToString("F"));
                order.OrderDate = order.OrderDate;
                order.OrderNumber = order.OrderNumber;
                order.UserCartId = order.UserCartId;

                Order updatedOrder = await _unitOfWork.OrderDal.UpdateAsync(order); //sipariş durumu onaylanır
                ConfirmOrderDto confirmOrderDto = _mapper.Map<ConfirmOrderDto>(updatedOrder);
                confirmOrderDto.TotalPrice = totalPrice;

                _unitOfWork.SaveChanges();
                await _unitOfWork.SaveChangesAsync();

                return confirmOrderDto;
            }
        }
    }
}
