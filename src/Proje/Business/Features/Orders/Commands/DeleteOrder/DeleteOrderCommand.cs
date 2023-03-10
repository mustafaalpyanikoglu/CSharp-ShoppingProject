using AutoMapper;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<DeletedOrderDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin, OrderDelete };

        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, DeletedOrderDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly OrderBusinessRules _orderBusinessRules;

            public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, 
                OrderBusinessRules orderBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _orderBusinessRules = orderBusinessRules;
            }

            public async Task<DeletedOrderDto> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                await _orderBusinessRules.OrderIdShouldExistWhenSelected(request.Id);

                Order mappedOrder = _mapper.Map<Order>(request);
                Order deletedOrder = await _unitOfWork.OrderDal.DeleteAsync(mappedOrder);
                DeletedOrderDto deleteOrderDto = _mapper.Map<DeletedOrderDto>(deletedOrder);

                await _unitOfWork.SaveChangesAsync();

                return deleteOrderDto;
            }
        }
    }
}
