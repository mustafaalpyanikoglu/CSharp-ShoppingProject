using AutoMapper;
using Business.Features.OrderDetails.Models;
using Business.Features.Orders.Models;
using Business.Features.Orders.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.Orders.Constants.Orders;
using static Entities.Constants.OperationClaims;

namespace Business.Features.Orders.Queries.GetListOrder
{
    public class GetListOrderQuery : IRequest<OrderListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] { Admin, OrderGet };

        public class GetListOrderQueryHanlder : IRequestHandler<GetListOrderQuery, OrderListModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListOrderQueryHanlder(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<OrderListModel> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Order> Orders = await _unitOfWork.OrderDal.GetListAsync(index: request.PageRequest.Page,
                                                                       size: request.PageRequest.PageSize,
                                                                       include: x => x.Include(c => c.UserCart.User));
                OrderListModel mappedOrderListModel = _mapper.Map<OrderListModel>(Orders);
                return mappedOrderListModel;

            }
        }
    }
}
