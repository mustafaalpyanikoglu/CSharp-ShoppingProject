using AutoMapper;
using Business.Features.Categories.Dtos;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Business.Features.Products.Rules;
using Business.Features.UserCarts.Rules;
using Business.Services.OrderService;
using Core;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using MediatR;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetails.Commands.CreateOrder
{
    public class CreateOrderDetailCommand:IRequest<CreatedOrderDetailDto>,ISecuredRequest
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string[] Roles => new[] { Admin, Customer, OrderDetailAdd };

        public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, CreatedOrderDetailDto>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDetailDal _orderDetailDal;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly ProductBusinessRules _productBusinessRules;

            public CreateOrderDetailCommandHandler(IMapper mapper, IOrderDetailDal orderDetailDal, 
                OrderDetailBusinessRules orderDetailBusinessRules, OrderBusinessRules orderBusinessRules, 
                ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _orderDetailDal = orderDetailDal;
                _orderDetailBusinessRules = orderDetailBusinessRules;
                _orderBusinessRules = orderBusinessRules;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<CreatedOrderDetailDto> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
            {
                await _orderBusinessRules.OrderIdShouldExistWhenSelected(request.OrderId);
                Product product = await _productBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.ProductId);
                await _orderDetailBusinessRules.TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(request.ProductId,request.Quantity);

                OrderDetail mappedOrderDetail = _mapper.Map<OrderDetail>(request);

                mappedOrderDetail.TotalPrice = product.Price * request.Quantity;

                OrderDetail createdOrderDetail = await _orderDetailDal.AddAsync(mappedOrderDetail);
                CreatedOrderDetailDto createOrderDetailDto = _mapper.Map<CreatedOrderDetailDto>(createdOrderDetail);

                return createOrderDetailDto;

            }
        }
    }
}
