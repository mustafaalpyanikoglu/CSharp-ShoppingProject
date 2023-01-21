using AutoMapper;
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
            private readonly IProductDal _productDal;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly UserCartBusinessRules _userCartBusinessRules;
            private readonly ProductBusinessRules _productBusinessRules;

            public CreateOrderCommandHandler(IMapper mapper, IOrderDal orderDal, IOrderService orderService, 
                IProductDal productDal, OrderBusinessRules orderBusinessRules, 
                UserCartBusinessRules userCartBusinessRules, ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _orderDal = orderDal;
                _orderService = orderService;
                _productDal = productDal;
                _orderBusinessRules = orderBusinessRules;
                _userCartBusinessRules = userCartBusinessRules;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                Product product = await _productBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.ProductId);
                await _userCartBusinessRules.UserCartIdShouldExistWhenSelected(request.UserCartId);
                await _orderBusinessRules.TheNumberOfProductsOrderedShouldNotBeMoreThanStock(request.ProductId, request.Quantity);

                Order mappedOrder = _mapper.Map<Order>(request);

                mappedOrder.OrderNumber = await _orderService.CreateOrderNumber();
                mappedOrder.Status = false;
                mappedOrder.OrderDate = Convert.ToDateTime(DateTime.Now.ToString("F"));
                mappedOrder.TotalPrice = request.Quantity * product.Price;

                Order createdOrder = await _orderDal.AddAsync(mappedOrder);

                CreatedOrderDto createOrderDto = _mapper.Map<CreatedOrderDto>(createdOrder);

                return createOrderDto;

            }
        }
    }
}
