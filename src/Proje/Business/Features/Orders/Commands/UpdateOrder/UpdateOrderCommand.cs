using AutoMapper;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Business.Features.Products.Rules;
using Business.Features.UserCarts.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<UpdatedOrderDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int UserCartId { get; set; }
        public string OrderNumber { get; set; }
        public bool Status { get; set; }

        public string[] Roles => new[] { Admin, OrderUpdate };

        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdatedOrderDto>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDal _orderDal;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly UserCartBusinessRules _userCartBusinessRules;

            public UpdateOrderCommandHandler(IMapper mapper, IOrderDal orderDal, OrderBusinessRules orderBusinessRules, 
                UserCartBusinessRules userCartBusinessRules)
            {
                _mapper = mapper;
                _orderDal = orderDal;
                _orderBusinessRules = orderBusinessRules;
                _userCartBusinessRules = userCartBusinessRules;
            }

            public async Task<UpdatedOrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                Order order = await _orderBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.Id);
                await _userCartBusinessRules.UserCartIdShouldExistWhenSelected(request.UserCartId);

                Order mappedOrder = _mapper.Map<Order>(request);

                mappedOrder.OrderDate = order.OrderDate;
                mappedOrder.ApprovalDate = order.ApprovalDate;

                Order UpdatedOrder = await _orderDal.UpdateAsync(mappedOrder);
                UpdatedOrderDto UpdateOrderDto = _mapper.Map<UpdatedOrderDto>(UpdatedOrder);

                return UpdateOrderDto;

            }
        }
    }
}
