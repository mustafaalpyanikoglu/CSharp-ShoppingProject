using AutoMapper;
using Business.Features.OrderDetails.Models;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Constants;
using Business.Features.Users.Rules;
using Business.Services.OrderDetailService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using DataAccess.Concrete.EfUnitOfWork;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetailDetails.Queries.GetListPastOrderDetailDetail
{
    public class GetListPastOrderDetailQuery : IRequest<UserPastOrderListModel>, ISecuredRequest
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new[] { Admin, Customer };

        public class GetListPastOrderDetailQueryHandler : IRequestHandler<GetListPastOrderDetailQuery, UserPastOrderListModel>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IOrderDetailService _orderDetailService;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public GetListPastOrderDetailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, 
                IOrderDetailService orderDetailService, UserBusinessRules userBusinessRules, 
                OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _orderDetailService = orderDetailService;
                _userBusinessRules = userBusinessRules;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<UserPastOrderListModel> Handle(GetListPastOrderDetailQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserIdMustBeAvailable(request.UserId);

                OrderDetail? orderDetail = await _unitOfWork.OrderDetailDal.GetAsync(o => o.Order.UserCart.UserId == request.UserId, include: c => c.Include(c => c.Order));
                if(orderDetail == null) throw new BusinessException(OrderMessages.PreviousOrderNotAvailable);
                await _orderDetailBusinessRules.OrderDetailIdShouldExistWhenSelected(orderDetail.Id);

                float totalPrice = await _orderDetailService.AmountUserCart(orderDetail.OrderId);

                IPaginate<OrderDetail> OrderDetails = await _unitOfWork.OrderDetailDal.GetListAsync(
                    o => o.Order.Status == true && o.Order.UserCart.User.Id == request.UserId,
                    include: c => c.Include(c => c.Product)
                                   .Include(c => c.Product.Category)
                                   .Include(c => c.Order)
                                   .Include(c => c.Order.UserCart)
                                   .Include(c => c.Order.UserCart.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);
                UserPastOrderListModel mappedGetListOrderDetailByUserCartDto = _mapper.Map<UserPastOrderListModel>(OrderDetails);
                mappedGetListOrderDetailByUserCartDto.AmountOfPayment = totalPrice;
                return mappedGetListOrderDetailByUserCartDto;
            }
        }
    }
}
