using AutoMapper;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Queries.GetByIdOrder
{
    public class GetByIdOrderQuery : IRequest<OrderDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new[] { Admin, OrderGet };

        public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQuery, OrderDto>
        {
            private readonly IOrderDal _OrderDal;
            private readonly IMapper _mapper;
            private readonly OrderBusinessRules _OrderBusinessRules;

            public GetByIdOrderQueryHandler(IOrderDal OrderDal, IMapper mapper, OrderBusinessRules OrderBusinessRules)
            {
                _OrderDal = OrderDal;
                _mapper = mapper;
                _OrderBusinessRules = OrderBusinessRules;
            }

            public async Task<OrderDto> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
            {
                await _OrderBusinessRules.OrderIdShouldExistWhenSelected(request.Id);

                Order? Order = await _OrderDal.GetAsync(m => m.Id == request.Id);
                OrderDto OrderDto = _mapper.Map<OrderDto>(Order);

                return OrderDto;
            }
        }
    }
}
