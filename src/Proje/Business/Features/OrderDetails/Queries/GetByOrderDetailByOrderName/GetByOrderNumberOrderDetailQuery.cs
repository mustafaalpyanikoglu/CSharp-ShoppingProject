using AutoMapper;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Models;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Rules;
using Business.Services.OrderDetailService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetails.Queries.GetByOrderDetailByOrderName
{
    public class GetListOrderDetailByOrderNameQuery : IRequest<OrderDetailListByUserCartModel>, ISecuredRequest
    {
        public string OrderNumber { get; set; }
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] { Admin, OrderDetailGet, Customer };

        public class GetListOrderDetailByOrderNameQueryHandler : IRequestHandler<GetListOrderDetailByOrderNameQuery, OrderDetailListByUserCartModel>
        {
            private readonly IOrderDetailDal _orderDetailDal;
            private readonly IMapper _mapper;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;
            private readonly IOrderDetailService _orderDetailService;

            public GetListOrderDetailByOrderNameQueryHandler(IOrderDetailDal orderDetailDal, IMapper mapper, OrderBusinessRules orderBusinessRules, OrderDetailBusinessRules orderDetailBusinessRules, IOrderDetailService orderDetailService)
            {
                _orderDetailDal = orderDetailDal;
                _mapper = mapper;
                _orderBusinessRules = orderBusinessRules;
                _orderDetailBusinessRules = orderDetailBusinessRules;
                _orderDetailService = orderDetailService;
            }

            public async Task<OrderDetailListByUserCartModel> Handle(GetListOrderDetailByOrderNameQuery request, CancellationToken cancellationToken)
            {
                await _orderBusinessRules.OrderNumberShouldExistWhenSelected(request.OrderNumber);

                OrderDetail? orderDetail = await _orderDetailDal.GetAsync(o => o.Order.OrderNumber == request.OrderNumber, include: c => c.Include(c => c.Order));
                await _orderDetailBusinessRules.OrderDetailIdShouldExistWhenSelected(orderDetail.Id);

                float totalPrice = await _orderDetailService.AmountUserCart(orderDetail.OrderId);

                IPaginate<OrderDetail>? OrderDetail = await _orderDetailDal.GetListAsync(
                    m => m.Order.OrderNumber == request.OrderNumber,
                    include: c => c.Include(c => c.Product)
                                   .Include(c => c.Product.Category)
                                   .Include(c => c.Order)
                                   .Include(c => c.Order.UserCart)
                                   .Include(c => c.Order.UserCart.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);
                OrderDetailListByUserCartModel orderDetailListDtoForCustomer = _mapper.Map<OrderDetailListByUserCartModel>(OrderDetail);
                orderDetailListDtoForCustomer.AmountOfPayment = totalPrice;
                return orderDetailListDtoForCustomer;
            }
        }
    }
}
