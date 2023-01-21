using AutoMapper;
using Business.Features.Orders.Models;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Queries.GetListPastOrder
{
    public class GetListPastOrderQuery : IRequest<OrderListByUserCartModel>//, ISecuredRequest
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class GetListPastOrderQueryHandler : IRequestHandler<GetListPastOrderQuery, OrderListByUserCartModel>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDal _orderDal;
            private readonly UserBusinessRules _userBusinessRules;

            public GetListPastOrderQueryHandler(IMapper mapper, IOrderDal orderDal, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _orderDal = orderDal;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<OrderListByUserCartModel> Handle(GetListPastOrderQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);

                IPaginate<Order> orders = await _orderDal.GetListAsync(o=> o.Status == true && o.UserCart.User.Id == request.UserId,
                                                                       include: c => c.Include(c => c.Product)
                                                                                      .Include(c => c.Product.Category)
                                                                                      .Include(c => c.UserCart.User),
                                                                       index: request.PageRequest.Page,
                                                                       size: request.PageRequest.PageSize);
                OrderListByUserCartModel mappedGetListOrderByUserCartDto = _mapper.Map<OrderListByUserCartModel>(orders);
                return mappedGetListOrderByUserCartDto;
            }
        }
    }
}
