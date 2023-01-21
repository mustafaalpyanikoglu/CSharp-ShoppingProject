using AutoMapper;
using Business.Features.OrderDetails.Models;
using Business.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetailDetails.Queries.GetListPastOrderDetailDetail
{
    public class GetListPastOrderDetailQuery : IRequest<OrderDetailListByUserCartModel>, ISecuredRequest
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class GetListPastOrderDetailQueryHandler : IRequestHandler<GetListPastOrderDetailQuery, OrderDetailListByUserCartModel>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDetailDal _OrderDetailDal;
            private readonly UserBusinessRules _userBusinessRules;

            public GetListPastOrderDetailQueryHandler(IMapper mapper, IOrderDetailDal OrderDetailDal, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _OrderDetailDal = OrderDetailDal;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<OrderDetailListByUserCartModel> Handle(GetListPastOrderDetailQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);

                IPaginate<OrderDetail> OrderDetails = await _OrderDetailDal.GetListAsync(
                    o => o.Order.Status == true && o.Order.UserCart.User.Id == request.UserId,
                    include: c => c.Include(c => c.Product)
                                   .Include(c => c.Product.Category)
                                   .Include(c => c.Order)
                                   .Include(c => c.Order.UserCart)
                                   .Include(c => c.Order.UserCart.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);
                OrderDetailListByUserCartModel mappedGetListOrderDetailByUserCartDto = _mapper.Map<OrderDetailListByUserCartModel>(OrderDetails);
                return mappedGetListOrderDetailByUserCartDto;
            }
        }
    }
}
