using AutoMapper;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Dtos;
using Business.Features.Products.Rules;
using Business.Features.UserCarts.Rules;
using Business.Services.OrderService;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;
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
            private readonly IOrderService _orderService;
            private readonly UserCartBusinessRules _userCartBusinessRules;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;
            private readonly IUnitOfWork _unitOfWork;

            public CreateOrderCommandHandler(IMapper mapper, IOrderService orderService, 
                UserCartBusinessRules userCartBusinessRules, ProductBusinessRules productBusinessRules, 
                OrderDetailBusinessRules orderDetailBusinessRules, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _orderService = orderService;
                _userCartBusinessRules = userCartBusinessRules;
                _productBusinessRules = productBusinessRules;
                _orderDetailBusinessRules = orderDetailBusinessRules;
                _unitOfWork = unitOfWork;
            }

            public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                await _userCartBusinessRules.UserCartIdShouldExistWhenSelected(request.UserCartId);
                Product product = await _productBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.ProductId);
                await _orderDetailBusinessRules.TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(request.ProductId, request.Quantity);
                
                Order order = await _unitOfWork.OrderDal.GetAsync(o => o.UserCartId == request.UserCartId && o.Status == false);
                string temorOrderNumber = order == null ? "" : order.OrderNumber; 
                Order currentOrder = await _unitOfWork.OrderDal.GetAsync(o=> o.OrderNumber == temorOrderNumber);

                if(currentOrder == null)
                {
                    Order createdOrder = await _unitOfWork.OrderDal.AddAsync(new Order
                    {
                        UserCartId = request.UserCartId,
                        OrderNumber = await _orderService.CreateOrderNumber(),
                        OrderDate = Convert.ToDateTime(DateTime.Now.ToString("F")),
                        Status = false,
                    });
                    //burda kaydetmek zorundayız çünkü OrderDetail tablosuna veri
                    //ekleyebilmek için FK alacağı tabloda veri olması lazım
                    await _unitOfWork.SaveChangesAsync();
                    OrderDetail orderDetail = await _unitOfWork.OrderDetailDal.AddAsync(new OrderDetail
                    {
                        OrderId = createdOrder.Id,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        TotalPrice = product.Price * request.Quantity
                    });

                    await _unitOfWork.SaveChangesAsync();

                    CreatedOrderDto createOrderDto = _mapper.Map<CreatedOrderDto>(createdOrder);
                    return createOrderDto;
                }
                else
                {
                    OrderDetail updatedOrderDetail = await _unitOfWork.OrderDetailDal.GetAsync(o => o.OrderId == currentOrder.Id && o.ProductId == request.ProductId);
                    if (updatedOrderDetail != null)
                    {
                        updatedOrderDetail.Quantity = updatedOrderDetail.Quantity + request.Quantity;
                        updatedOrderDetail.TotalPrice = updatedOrderDetail.Quantity * product.Price;
                        OrderDetail mappedUpdatedOrderDetail2 = await _unitOfWork.OrderDetailDal.UpdateAsync(updatedOrderDetail);
                    }
                    else
                    {
                        OrderDetail addedOrderDetail = await _unitOfWork.OrderDetailDal.AddAsync(new OrderDetail
                        {
                            OrderId = currentOrder.Id,
                            ProductId = request.ProductId,
                            Quantity = request.Quantity,
                            TotalPrice = product.Price * request.Quantity
                        });
                    }
                    await _unitOfWork.SaveChangesAsync();
                    CreatedOrderDto createOrderDto = _mapper.Map<CreatedOrderDto>(currentOrder);
                    return createOrderDto;
                }
            }
        }
    }
}
