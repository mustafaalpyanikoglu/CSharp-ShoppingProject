using AutoMapper;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Business.Services.PurseService;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
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
            private readonly IOrderDal _orderDal;
            private readonly IUserCartDal _userCartDal;
            private readonly IPurseDal _purseDal;
            private readonly IPurseService _purseService;
            private readonly IProductDal _productDal;
            private readonly OrderBusinessRules _orderBusinessRules;

            public ConfirmOrderCommandHandler(IMapper mapper, IOrderDal orderDal, 
                IUserCartDal userCartDal, IPurseDal purseDal, IPurseService purseService, 
                IProductDal productDal, OrderBusinessRules orderBusinessRules)
            {
                _mapper = mapper;
                _orderDal = orderDal;
                _userCartDal = userCartDal;
                _purseDal = purseDal;
                _purseService = purseService;
                _productDal = productDal;
                _orderBusinessRules = orderBusinessRules;
            }

            public async Task<ConfirmOrderDto> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
            {
                Order order = await _orderBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.OrderId);

                await _orderBusinessRules.OrderStatusMustBeFalse(order.Status);

                UserCart? userCart = await _userCartDal.GetAsync(u => u.Id == order.UserCartId);
                Product? product = await _productDal.GetAsync(p=> p.Id == order.ProductId);
                Purse? purse = await _purseDal.GetAsync(p => p.UserId == userCart.UserId);

                
                await _purseService.SpendMoney(purse,order.TotalPrice); //Kullanıcının yeterli parası varsa parası azalır


                product.Quantity -= order.Quantity; //stokda ki ürün miktarı azalır
                await _productDal.UpdateAsync(product);


                order.Status = true;
                order.ApprovalDate = Convert.ToDateTime(DateTime.Now.ToString("F"));
                order.OrderDate = order.OrderDate;
                order.OrderNumber = order.OrderNumber;
                order.UserCartId = order.UserCartId;
                order.ProductId = order.ProductId;
                order.TotalPrice = order.TotalPrice;
                order.Quantity = order.Quantity;

                Order updatedOrder = await _orderDal.UpdateAsync(order); //sipariş durumu onaylanır
                ConfirmOrderDto confirmOrderDto = _mapper.Map<ConfirmOrderDto>(updatedOrder);

                confirmOrderDto.ProductName = product.Name; //kullanıcının aldığı ürünleri görmesi için
                confirmOrderDto.ProductPrice = product.Price;

                return confirmOrderDto;
            }
        }
    }
}
