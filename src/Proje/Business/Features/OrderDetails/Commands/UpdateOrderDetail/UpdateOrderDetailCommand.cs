using AutoMapper;
using Business.Features.Orders.Rules;
using Business.Features.Products.Rules;
using Core.Application.Pipelines.Authorization;
using Entities.Concrete;
using MediatR;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Rules;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;
using DataAccess.Concrete.EfUnitOfWork;

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
                await _orderBusinessRules.OrderStatusMustBeFalse(request.OrderId);

                OrderDetail? orderDetail = await _unitOfWork.OrderDetailDal.GetAsync(o => o.Id== request.Id);

                Order? updatedOrder = await _unitOfWork.OrderDal.GetAsync(o => o.Id == request.OrderId);
                updatedOrder.OrderAmount -= orderDetail.TotalPrice;


                OrderDetail mappedOrderDetail = _mapper.Map<OrderDetail>(request);

                mappedOrderDetail.TotalPrice = product.Price * request.Quantity;

                OrderDetail updatedOrderDetail = await _unitOfWork.OrderDetailDal.UpdateAsync(mappedOrderDetail);
                UpdatedOrderDetailDto updatedOrderDetailDto = _mapper.Map<UpdatedOrderDetailDto>(updatedOrderDetail);
                
                updatedOrder.OrderAmount += product.Price * request.Quantity;
                await _unitOfWork.OrderDal.UpdateAsync(updatedOrder);
               
                await _unitOfWork.SaveChangesAsync();

                return updatedOrderDetailDto;
            }
        }
    }
}
