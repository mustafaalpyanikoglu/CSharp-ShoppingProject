using AutoMapper;
using Business.Features.OrderDetails.Dtos;
using Business.Features.OrderDetails.Rules;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

            public UpdateOrderDetailForCustomerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, 
                OrderDetailBusinessRules orderDetailBusinessRules)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _orderDetailBusinessRules = orderDetailBusinessRules;
            }

            public async Task<UpdatedOrderDetailForCustomerDto> Handle(UpdateOrderDetailForCustomerCommand request, CancellationToken cancellationToken)
            {
                OrderDetail orderDetail =  await _orderDetailBusinessRules.ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(request.Id);
                await _orderDetailBusinessRules.TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(orderDetail.ProductId, request.Quantity + orderDetail.Quantity);

                Product product = await _unitOfWork.ProductDal.GetAsync(p => p.Id == orderDetail.ProductId);

                if(request.Quantity == 0) //Sepetteki ürün sıfırlanıyorsa ürün sepetten silinmeli
                {
                    OrderDetail deletedOrderDetail = await _unitOfWork.OrderDetailDal.DeleteAsync(orderDetail);
                    UpdatedOrderDetailForCustomerDto deleteOrderDetailForCustomerDto = _mapper.Map<UpdatedOrderDetailForCustomerDto>(deletedOrderDetail);

                    await _unitOfWork.SaveChangesAsync();
                    
                    return deleteOrderDetailForCustomerDto;
                }

                orderDetail.TotalPrice = product.Price * request.Quantity;
                orderDetail.ProductId = orderDetail.ProductId;
                orderDetail.Quantity = request.Quantity;
                orderDetail.Id = request.Id;

                OrderDetail updatedOrderDetail = await _unitOfWork.OrderDetailDal.UpdateAsync(orderDetail);
                UpdatedOrderDetailForCustomerDto updateOrderDetailDto = _mapper.Map<UpdatedOrderDetailForCustomerDto>(updatedOrderDetail);

                await _unitOfWork.SaveChangesAsync();

                return updateOrderDetailDto;
            }
        }
    }
}
