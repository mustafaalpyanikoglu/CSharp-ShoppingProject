using AutoMapper;
using Business.Features.OrderDetails.Models;
using Business.Features.OrderDetails.Rules;
using Business.Features.Orders.Rules;
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

namespace Business.Features.OrderDetails.Queries.GetByOrderDetailByOrderName
{
    public class GetListOrderDetailByOrderNameQuery : IRequest<OrderDetailListByUserCartModel>, ISecuredRequest
    {
        public string OrderNumber { get; set; }
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] { Admin, OrderDetailGet, Customer };

        public class GetListOrderDetailByOrderNameQueryHandler : IRequestHandler<GetListOrderDetailByOrderNameQuery, OrderDetailListByUserCartModel>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDetailService _orderDetailService;
            private readonly IUnitOfWork _unitOfWork;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public GetListOrderDetailByOrderNameQueryHandler(IMapper mapper, IOrderDetailService orderDetailService, 
                IUnitOfWork unitOfWork, OrderBusinessRules orderBusinessRules, OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _orderDetailService = orderDetailService;
                _unitOfWork = unitOfWork;
                _orderBusinessRules = orderBusinessRules;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<OrderDetailListByUserCartModel> Handle(GetListOrderDetailByOrderNameQuery request, CancellationToken cancellationToken)
            {
                await _orderBusinessRules.OrderNumberShouldExistWhenSelected(request.OrderNumber);

                IPaginate<OrderDetail>? orderDetails = await _unitOfWork.OrderDetailDal.GetListAsync(
                    m => m.Order.OrderNumber == request.OrderNumber,
                    include: c => c.Include(c => c.Product)
                                   .Include(c => c.Product.Category)
                                   .Include(c => c.Order)
                                   .Include(c => c.Order.UserCart)
                                   .Include(c => c.Order.UserCart.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                await _orderDetailBusinessRules.ThereMustBeDataInList(orderDetails);

                OrderDetailListByUserCartModel orderDetailListDtoForCustomer = _mapper.Map<OrderDetailListByUserCartModel>(orderDetails);
                return orderDetailListDtoForCustomer;
            }
        }
    }
}
