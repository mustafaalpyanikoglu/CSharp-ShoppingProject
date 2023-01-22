using AutoMapper;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetails.Queries.GetByIdOrderDetail
{
    public class GetByIdOrderDetailQuery : IRequest<OrderDetailDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new[] { Admin, OrderDetailGet };

        public class GetByIdOrderDetailQueryHandler : IRequestHandler<GetByIdOrderDetailQuery, OrderDetailDto>
        {
            private readonly IOrderDetailDal _orderDetailDal;
            private readonly IMapper _mapper;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public GetByIdOrderDetailQueryHandler(IOrderDetailDal orderDetailDal, IMapper mapper, OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _orderDetailDal = orderDetailDal;
                _mapper = mapper;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<OrderDetailDto> Handle(GetByIdOrderDetailQuery request, CancellationToken cancellationToken)
            {
                await _orderDetailBusinessRules.OrderDetailIdShouldExistWhenSelected(request.Id);

                OrderDetail? OrderDetail = await _orderDetailDal.GetAsync(
                    m => m.Id == request.Id,
                    include: c => c.Include(c => c.Product)
                                   .Include(c => c.Product.Category)
                                   .Include(c => c.Order)
                                   .Include(c => c.Order.UserCart)
                                   .Include(c => c.Order.UserCart.User)
                                   );
                OrderDetailDto OrderDetailDto = _mapper.Map<OrderDetailDto>(OrderDetail);

                return OrderDetailDto;
            }
        }
    }
}
