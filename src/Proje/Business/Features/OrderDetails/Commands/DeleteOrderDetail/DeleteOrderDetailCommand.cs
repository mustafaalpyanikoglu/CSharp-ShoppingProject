using AutoMapper;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetails.Commands.DeleteOrder
{
    public class DeleteOrderDetailCommand : IRequest<DeletedOrderDetailDto>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new[] { Admin, OrderDetailDelete, Customer };

        public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, DeletedOrderDetailDto>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public DeleteOrderDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<DeletedOrderDetailDto> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
            {
                await _orderDetailBusinessRules.OrderDetailIdShouldExistWhenSelected(request.Id);

                OrderDetail mappedOrderDetail = _mapper.Map<OrderDetail>(request);
                OrderDetail DeletedOrderDetail = await _unitOfWork.OrderDetailDal.DeleteAsync(mappedOrderDetail);
                DeletedOrderDetailDto DeleteOrderDetailDto = _mapper.Map<DeletedOrderDetailDto>(DeletedOrderDetail);

                await _unitOfWork.SaveChangesAsync();

                return DeleteOrderDetailDto;
            }
        }
    }
}
