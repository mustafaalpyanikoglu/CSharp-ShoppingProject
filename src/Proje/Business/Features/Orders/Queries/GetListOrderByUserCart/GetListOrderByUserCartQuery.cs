using AutoMapper;
using Business.Features.Orders.Models;
using Business.Features.UserCarts.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Queries.GetListPastOrder
{
    public class GetListOrderByUserCartQuery:IRequest<OrderListByUserCartModel>//,ISecuredRequest
    {
        public int UserCartId { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class GetListOrderByUserCartQueryHandler : IRequestHandler<GetListOrderByUserCartQuery, OrderListByUserCartModel>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDal _orderDal;
            private readonly UserCartBusinessRules _userCartBusinessRules;

            public GetListOrderByUserCartQueryHandler(IMapper mapper, IOrderDal orderDal, UserCartBusinessRules userCartBusinessRules)
            {
                _mapper = mapper;
                _orderDal = orderDal;
                _userCartBusinessRules = userCartBusinessRules;
            }

            public async Task<OrderListByUserCartModel> Handle(GetListOrderByUserCartQuery request, CancellationToken cancellationToken)
            {
                await _userCartBusinessRules.UserCartIdShouldExistWhenSelected(request.UserCartId);

                IPaginate<Order> orders = await _orderDal.GetListAsync(o=> o.UserCartId == request.UserCartId && o.Status ==false,
                                                                       include: c => c.Include(c => c.Product)
                                                                                      .Include(c => c.Product.Category)
                                                                                      .Include(c=>c.UserCart.User),
                                                                       index: request.PageRequest.Page,
                                                                       size: request.PageRequest.PageSize);
                OrderListByUserCartModel mappedGetListOrderByUserCartDto = _mapper.Map<OrderListByUserCartModel>(orders);
                return mappedGetListOrderByUserCartDto;
            }
        }
    }
}
