using AutoMapper;
using Business.Features.OrderDetails.Models;
using Business.Features.OrderDetails.Rules;
using Business.Features.Users.Rules;
using Business.Services.OrderDetailService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetailDetails.Queries.GetListOrderDetailDetailByUserCart
{
    public class GetListOrderDetailByUserCartQuery : IRequest<OrderDetailListByUserCartModel>,ISecuredRequest
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class GetListOrderDetailByUserCartQueryHandler : IRequestHandler<GetListOrderDetailByUserCartQuery, OrderDetailListByUserCartModel>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IOrderDetailService _orderDetailService;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public GetListOrderDetailByUserCartQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, 
                IOrderDetailService orderDetailService, UserBusinessRules userBusinessRules, 
                OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _orderDetailService = orderDetailService;
                _userBusinessRules = userBusinessRules;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<OrderDetailListByUserCartModel> Handle(GetListOrderDetailByUserCartQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);
                
                OrderDetail? orderDetail = await _unitOfWork.OrderDetailDal.GetAsync(o => o.Order.UserCart.UserId == request.UserId && o.Order.Status == false, include: c => c.Include(c => c.Order));

                await _orderDetailBusinessRules.IsOrderDetailNull(orderDetail);
                await _orderDetailBusinessRules.OrderDetailIdShouldExistWhenSelected(orderDetail.Id);


                IPaginate<OrderDetail> OrderDetails = await _unitOfWork.OrderDetailDal.GetListAsync(
                    o => o.Order.UserCart.UserId == request.UserId && o.Order.Status == false,
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
