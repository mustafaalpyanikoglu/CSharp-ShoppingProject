using AutoMapper;
using Business.Features.Orders.Rules;
using Business.Features.Products.Rules;
using Core.Application.Pipelines.Authorization;
using Entities.Concrete;
using MediatR;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Rules;
using DataAccess.Concrete.Contexts;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetails.Commands.UpdateOrder
{
    public class UpdateOrderDetailCommand : IRequest<UpdatedOrderDetailDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string[] Roles => new[] { Admin, OrderDetailUpdate };

        public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, UpdatedOrderDetailDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly ProductBusinessRules _productBusinessRules;

            public UpdateOrderDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, 
                OrderDetailBusinessRules orderDetailBusinessRules, OrderBusinessRules orderBusinessRules, 
                ProductBusinessRules productBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _orderDetailBusinessRules = orderDetailBusinessRules;
                _orderBusinessRules = orderBusinessRules;
                _productBusinessRules = productBusinessRules;
            }

            public async Task<UpdatedOrderDetailDto> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
            {
                await _orderBusinessRules.OrderIdShouldExistWhenSelected(request.OrderId);
                Product product = await _productBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.ProductId);
                await _orderDetailBusinessRules.TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(request.ProductId, request.Quantity);

                OrderDetail mappedOrderDetail = _mapper.Map<OrderDetail>(request);

                mappedOrderDetail.TotalPrice = product.Price * request.Quantity;

                OrderDetail UpdatedOrderDetail = await _unitOfWork.OrderDetailDal.UpdateAsync(mappedOrderDetail);
                UpdatedOrderDetailDto createOrderDetailDto = _mapper.Map<UpdatedOrderDetailDto>(UpdatedOrderDetail);

                await _unitOfWork.SaveChangesAsync();

                return createOrderDetailDto;
            }
        }
    }
}
