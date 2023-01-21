using AutoMapper;
using Business.Features.OrderDetails.Models;
using Business.Features.UserCarts.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetailDetails.Queries.GetListOrderDetailDetailByUserCart
{
    public class GetListOrderDetailByUserCartQuery : IRequest<OrderDetailListByUserCartModel>,ISecuredRequest
    {
        public int UserCartId { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class GetListOrderDetailByUserCartQueryHandler : IRequestHandler<GetListOrderDetailByUserCartQuery, OrderDetailListByUserCartModel>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDetailDal _orderDetailDal;
            private readonly UserCartBusinessRules _userCartBusinessRules;

            public GetListOrderDetailByUserCartQueryHandler(IMapper mapper, IOrderDetailDal orderDetailDal, UserCartBusinessRules userCartBusinessRules)
            {
                _mapper = mapper;
                _orderDetailDal = orderDetailDal;
                _userCartBusinessRules = userCartBusinessRules;
            }

            public async Task<OrderDetailListByUserCartModel> Handle(GetListOrderDetailByUserCartQuery request, CancellationToken cancellationToken)
            {
                await _userCartBusinessRules.UserCartIdShouldExistWhenSelected(request.UserCartId);

                IPaginate<OrderDetail> OrderDetails = await _orderDetailDal.GetListAsync(
                    o => o.Order.UserCartId == request.UserCartId && o.Order.Status == false,
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
