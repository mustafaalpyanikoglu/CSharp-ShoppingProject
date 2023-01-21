using AutoMapper;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Business.Services.PurseService;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Commands.ConfirmOrder
{
    public class ConfirmOrderCommand:IRequest<ConfirmOrderDto>//,ISecuredRequest
    {
        public int OrderId { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, ConfirmOrderDto>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDal _orderDal;
            private readonly IUserCartDal _userCartDal;
            private readonly IPurseDal _purseDal;
            private readonly IPurseService _purseService;
            private readonly IProductDal _productDal;
            private readonly IOrderDetailDal _orderDetailDal;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public ConfirmOrderCommandHandler(IMapper mapper, IOrderDal orderDal, IUserCartDal userCartDal, IPurseDal purseDal, IPurseService purseService, IProductDal productDal, IOrderDetailDal orderDetailDal, OrderBusinessRules orderBusinessRules, OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _orderDal = orderDal;
                _userCartDal = userCartDal;
                _purseDal = purseDal;
                _purseService = purseService;
                _productDal = productDal;
                _orderDetailDal = orderDetailDal;
                _orderBusinessRules = orderBusinessRules;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<ConfirmOrderDto> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
            {
                Order order = await _orderBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.OrderId);
                await _orderDetailBusinessRules.IsThereAnyProductInTheCart(request.OrderId);
                await _orderBusinessRules.OrderStatusMustBeFalse(order.Status);

                Purse? purse = await _purseDal.GetByUserCartId(order.UserCartId);
                
                List<OrderDetail> orderDetails = _orderDetailDal.GetAll(o=> o.OrderId == request.OrderId);
                Product? product = await _productDal.GetAsync(p => p.Id == orderDetails.SingleOrDefault().ProductId);

                float totalPrice = _orderDetailDal.CalculateAmountInCart(request.OrderId);
                await _purseService.SpendMoney(purse, totalPrice); //Kullanıcının yeterli parası varsa parası azalır

                foreach (var item in orderDetails) //sipariş alınan her ürün için stokda ki miktarı kontrol edilir varsa azaltılır
                {
                    Product updatedProduct = await _orderDetailBusinessRules.TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(item.ProductId, item.Quantity);
                    updatedProduct.Quantity -= item.Quantity;
                    await _productDal.UpdateAsync(updatedProduct);
                }

                order.Status = true;
                order.OrderDate = order.OrderDate;
                order.ApprovalDate = Convert.ToDateTime(DateTime.Now.ToString("F"));
                order.OrderNumber = order.OrderNumber;
                order.UserCartId = order.UserCartId;

                Order updatedOrder = await _orderDal.UpdateAsync(order); //sipariş durumu onaylanır
                ConfirmOrderDto confirmOrderDto = _mapper.Map<ConfirmOrderDto>(updatedOrder);
                confirmOrderDto.OrderNumber = updatedOrder.OrderNumber;
                confirmOrderDto.TotalPrice = totalPrice;
                confirmOrderDto.ProductName = product.Name; //kullanıcının aldığı ürünleri görmesi için
                confirmOrderDto.ProductPrice = product.Price;

                return confirmOrderDto;
            }
        }
    }
}
