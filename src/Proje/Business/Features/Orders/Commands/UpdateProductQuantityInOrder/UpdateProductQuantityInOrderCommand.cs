using AutoMapper;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Commands.UpdateProductQuantityInOrder
{
    public class UpdateProductQuantityInOrderCommand:IRequest<UpdateProductQuantityInOrderDto>//,ISecuredRequest
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        
        public string[] Roles => new[] { Admin, Customer };

        public class UpdateProductQuantityInOrderCommandHandler : IRequestHandler<UpdateProductQuantityInOrderCommand, UpdateProductQuantityInOrderDto>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDal _orderDal;
            private readonly IProductDal _productDal;
            private readonly OrderBusinessRules _orderBusinessRules;

            public UpdateProductQuantityInOrderCommandHandler(IMapper mapper, IOrderDal orderDal, IProductDal productDal, OrderBusinessRules orderBusinessRules)
            {
                _mapper = mapper;
                _orderDal = orderDal;
                _productDal = productDal;
                _orderBusinessRules = orderBusinessRules;
            }

            public async Task<UpdateProductQuantityInOrderDto> Handle(UpdateProductQuantityInOrderCommand request, CancellationToken cancellationToken)
            {
                Order order = await _orderBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.Id);
                await _orderBusinessRules.TheNumberOfProductsOrderedShouldNotBeMoreThanStock(order.ProductId, request.Quantity);
                
                Product? product = await _productDal.GetAsync(p => p.Id == order.ProductId);

                if(request.Quantity == 0)
                {
                    Order deletedOrder = await _orderDal.DeleteAsync(order);
                    UpdateProductQuantityInOrderDto deletedUpdateProductQuantityInOrderDto = _mapper.Map<UpdateProductQuantityInOrderDto>(deletedOrder);
                    return deletedUpdateProductQuantityInOrderDto;
                }

                order.Quantity = request.Quantity;
                order.TotalPrice = request.Quantity * product.Price;

                Order updatedOrder = await _orderDal.UpdateAsync(order);
                UpdateProductQuantityInOrderDto updatedUpdateProductQuantityInOrderDto = _mapper.Map<UpdateProductQuantityInOrderDto>(updatedOrder);

                return updatedUpdateProductQuantityInOrderDto;
            }
        }
    }
}
