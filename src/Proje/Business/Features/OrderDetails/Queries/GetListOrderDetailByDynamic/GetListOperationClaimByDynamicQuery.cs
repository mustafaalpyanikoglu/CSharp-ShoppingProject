using AutoMapper;
using Business.Features.OrderDetails.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetailDetails.Queries.GetListOrderDetailDetailByDynamic
{
    public class GetListOrderDetailByDynamicQuery : IRequest<OrderDetailListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public string[] Roles => new[] { Admin, OrderDetailGet };

        public class GetListOrderDetailByDynamicQueryHandler : IRequestHandler<GetListOrderDetailByDynamicQuery, OrderDetailListModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListOrderDetailByDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<OrderDetailListModel> Handle(GetListOrderDetailByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OrderDetail> OrderDetails = await _unitOfWork.OrderDetailDal.GetListByDynamicAsync(
                                      request.Dynamic, 
                                      include: c => c.Include(c => c.Product)
                                                    .Include(c => c.Product.Category)
                                                    .Include(c => c.Order)
                                                    .Include(c => c.Order.UserCart)
                                                    .Include(c => c.Order.UserCart.User),
                                      request.PageRequest.Page,
                                      request.PageRequest.PageSize);
                OrderDetailListModel mappedOrderDetailListModel = _mapper.Map<OrderDetailListModel>(OrderDetails);
                return mappedOrderDetailListModel;
            }
        }
    }
}
