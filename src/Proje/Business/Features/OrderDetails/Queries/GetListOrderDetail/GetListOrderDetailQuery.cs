using AutoMapper;
using Business.Features.OrderDetails.Models;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetailDetails.Queries.GetListOrderDetailDetail
{
    public class GetListOrderDetailQuery : IRequest<OrderDetailListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] { Admin, OrderDetailGet };

        public class GetListOrderDetailQueryHanlder : IRequestHandler<GetListOrderDetailQuery, OrderDetailListModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetListOrderDetailQueryHanlder(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<OrderDetailListModel> Handle(GetListOrderDetailQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OrderDetail> orderDetails = await _unitOfWork.OrderDetailDal.GetListAsync(
                    include: c => c.Include(c => c.Product)
                                   .Include(c => c.Product.Category)
                                   .Include(c => c.Order)
                                   .Include(c => c.Order.UserCart)
                                   .Include(c => c.Order.UserCart.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);
                OrderDetailListModel mappedOrderDetailListModel = _mapper.Map<OrderDetailListModel>(orderDetails);
                return mappedOrderDetailListModel;

            }
        }
    }
}
