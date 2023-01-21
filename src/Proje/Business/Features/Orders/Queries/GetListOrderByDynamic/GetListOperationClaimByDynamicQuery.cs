using AutoMapper;
using Business.Features.OrderDetails.Models;
using Business.Features.Orders.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Queries.GetListOrderByDynamic
{
    public class GetListOrderByDynamicQuery:IRequest<OrderListModel>//, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public string[] Roles => new[] { Admin, OrderGet };

        public class GetListOrderByDynamicQueryHandler : IRequestHandler<GetListOrderByDynamicQuery, OrderListModel>
        {
            private readonly IOrderDal _OrderDal;
            private readonly IMapper _mapper;

            public GetListOrderByDynamicQueryHandler(IOrderDal OrderDal, IMapper mapper)
            {
                _OrderDal = OrderDal;
                _mapper = mapper;
            }

            public async Task<OrderListModel> Handle(GetListOrderByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Order> Orders = await _OrderDal.GetListByDynamicAsync(
                                      request.Dynamic,
                                      include: x => x.Include(c => c.UserCart.User),
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                OrderListModel mappedOrderListModel = _mapper.Map<OrderListModel>(Orders);
                return mappedOrderListModel;
            }
        }
    }
}
