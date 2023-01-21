using AutoMapper;
using Business.Features.OrderDetails.Commands.UpdateOrder;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Business.Features.Products.Rules;
using Business.Features.UserCarts.Rules;
using Business.Services.OrderService;
using Core;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand:IRequest<CreatedOrderDto>,ISecuredRequest
    {
        public int UserCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string[] Roles => new[] { Admin, Customer, OrderAdd };

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderDto>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDal _orderDal;
            private readonly IOrderService _orderService;
            private readonly IOrderDetailDal _orderDetailDal;
            private readonly IProductDal _productDal;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly UserCartBusinessRules _userCartBusinessRules;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public CreateOrderCommandHandler(IMapper mapper, IOrderDal orderDal, IOrderService orderService, IOrderDetailDal orderDetailDal, IProductDal productDal, OrderBusinessRules orderBusinessRules, UserCartBusinessRules userCartBusinessRules, ProductBusinessRules productBusinessRules, OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _orderDal = orderDal;
                _orderService = orderService;
                _orderDetailDal = orderDetailDal;
                _productDal = productDal;
                _orderBusinessRules = orderBusinessRules;
                _userCartBusinessRules = userCartBusinessRules;
                _productBusinessRules = productBusinessRules;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                await _userCartBusinessRules.UserCartIdShouldExistWhenSelected(request.UserCartId);
                Product product = await _productBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.ProductId);
                await _orderDetailBusinessRules.TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(request.ProductId, request.Quantity);
                
                Order order = await _orderDal.GetAsync(o => o.UserCartId == request.UserCartId && o.Status == false);
                string temorOrderNumber = order == null ? "" : order.OrderNumber; 
                Order currentOrder = await _orderDal.GetAsync(o=> o.OrderNumber == temorOrderNumber);

                if(currentOrder == null)
                {
                    Order createdOrder = await _orderDal.AddAsync(new Order
                    {
                        UserCartId = request.UserCartId,
                        OrderNumber = await _orderService.CreateOrderNumber(),
                        OrderDate = Convert.ToDateTime(DateTime.Now.ToString("F")),
                        Status = false,
                    });

                    OrderDetail orderDetail = await _orderDetailDal.AddAsync(new OrderDetail
                    {
                        OrderId = createdOrder.Id,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        TotalPrice = product.Price * request.Quantity
                    });
                    CreatedOrderDto createOrderDto = _mapper.Map<CreatedOrderDto>(createdOrder);
                    return createOrderDto;
                }
                else
                {
                    OrderDetail updatedOrderDetail = await _orderDetailDal.GetAsync(o => o.OrderId == currentOrder.Id && o.ProductId == request.ProductId);
                    if (updatedOrderDetail != null)
                    {
                        updatedOrderDetail.Quantity = updatedOrderDetail.Quantity + request.Quantity;
                        updatedOrderDetail.TotalPrice = updatedOrderDetail.Quantity * product.Price;
                        OrderDetail mappedUpdatedOrderDetail2 = await _orderDetailDal.UpdateAsync(updatedOrderDetail);
                    }
                    else
                    {
                        OrderDetail addedOrderDetail = await _orderDetailDal.AddAsync(new OrderDetail
                        {
                            OrderId = currentOrder.Id,
                            ProductId = request.ProductId,
                            Quantity = request.Quantity,
                            TotalPrice = product.Price * request.Quantity
                        });
                    }
                    CreatedOrderDto createOrderDto = _mapper.Map<CreatedOrderDto>(currentOrder);
                    return createOrderDto;
                }
            }
        }
    }
}
