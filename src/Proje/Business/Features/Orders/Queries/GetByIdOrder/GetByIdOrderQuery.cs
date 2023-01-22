using AutoMapper;
using Business.Features.Orders.Dtos;
using Business.Features.Orders.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly OrderBusinessRules _OrderBusinessRules;

            public GetByIdOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
                OrderBusinessRules orderBusinessRules)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _OrderBusinessRules = orderBusinessRules;
            }

            public async Task<OrderDto> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
            {
                await _OrderBusinessRules.OrderIdShouldExistWhenSelected(request.Id);

                Order? Order = await _unitOfWork.OrderDal.GetAsync(m => m.Id == request.Id,
                                                        include: x => x.Include(c => c.UserCart.User));
                OrderDto OrderDto = _mapper.Map<OrderDto>(Order);

                return OrderDto;
            }
        }
    }
}
