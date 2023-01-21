using AutoMapper;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using static Business.Features.OrderDetails.Constants.OperationClaims;
using static Entities.Constants.OperationClaims;

namespace Business.Features.OrderDetails.Commands.UpdateOrderDetailForCustomer
{
    public class UpdateOrderDetailForCustomerCommand:IRequest<UpdatedOrderDetailForCustomerDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public string[] Roles => new[] { Admin, Customer,OrderDetailUpdate };

        public class UpdateOrderDetailForCustomerCommandHandler : IRequestHandler<UpdateOrderDetailForCustomerCommand, UpdatedOrderDetailForCustomerDto>
        {
            private readonly IMapper _mapper;
            private readonly IOrderDetailDal _orderDetailDal;
            private readonly IProductDal _productDal;
            private readonly IOrderDal _orderDal;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public UpdateOrderDetailForCustomerCommandHandler(IMapper mapper, IOrderDetailDal orderDetailDal, 
                IProductDal productDal, IOrderDal orderDal, OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _orderDetailDal = orderDetailDal;
                _productDal = productDal;
                _orderDal = orderDal;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<UpdatedOrderDetailForCustomerDto> Handle(UpdateOrderDetailForCustomerCommand request, CancellationToken cancellationToken)
            {
                OrderDetail orderDetail =  await _orderDetailBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.Id);
                await _orderDetailBusinessRules.TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(orderDetail.ProductId, request.Quantity + orderDetail.Quantity);

                Product product = await _productDal.GetAsync(p => p.Id == orderDetail.ProductId);

                if(request.Quantity == 0) //Sepetteki ürün sıfırlanıyorsa ürün sepetten silinmeli
                {
                    List<OrderDetail> orderDetails = _orderDetailDal.GetAll(o => o.OrderId == request.Id);
                    if(orderDetails.Count > 1) //Bu septte birden fazla ürün varsa sadece o ürünü siler
                    {
                        OrderDetail deletedOrderDetail = await _orderDetailDal.DeleteAsync(orderDetail);
                        UpdatedOrderDetailForCustomerDto deleteOrderDetailForCustomerDto = _mapper.Map<UpdatedOrderDetailForCustomerDto>(deletedOrderDetail);
                        return deleteOrderDetailForCustomerDto;
                    }
                    else // Bu sepette 1 tane ürün varsa o ürünü siler ve oluşturulan siparişi de siler
                    {
                        Order order = await _orderDal.GetAsync(o => o.Id == orderDetail.OrderId);
                        await _orderDal.DeleteAsync(order);

                        OrderDetail deletedOrderDetail = await _orderDetailDal.DeleteAsync(orderDetail);
                        UpdatedOrderDetailForCustomerDto deleteOrderDetailForCustomerDto = _mapper.Map<UpdatedOrderDetailForCustomerDto>(deletedOrderDetail);
                        return deleteOrderDetailForCustomerDto;
                    }
                }

                OrderDetail mappedOrderDetail = _mapper.Map<OrderDetail>(request);

                mappedOrderDetail.TotalPrice = product.Price * request.Quantity; 
                mappedOrderDetail.ProductId = orderDetail.ProductId;

                OrderDetail updatedOrderDetail = await _orderDetailDal.UpdateAsync(mappedOrderDetail);
                UpdatedOrderDetailForCustomerDto updateOrderDetailDto = _mapper.Map<UpdatedOrderDetailForCustomerDto>(updatedOrderDetail);

                return updateOrderDetailDto;
            }
        }
    }
}
