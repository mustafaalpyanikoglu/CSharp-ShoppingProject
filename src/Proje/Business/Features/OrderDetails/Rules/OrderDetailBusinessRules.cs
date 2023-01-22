using static Business.Features.OrderDetails.Constants.OrderDetailMessages;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Entities.Concrete;
using DataAccess.Concrete.Contexts;

namespace Business.Features.OrderDetails.Rules
{
    public class OrderDetailBusinessRules:BaseBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OrderDetailIdShouldExistWhenSelected(int? id)
        {
            OrderDetail? result = await _unitOfWork.OrderDetailDal.GetAsync(b => b.Id == id);
            if (result ==null) throw new BusinessException(OrderDetailNotFound);
        }
        public Task IsOrderDetailNull(OrderDetail orderDetail)
        {
            if (orderDetail == null) throw new BusinessException(OrderDetailNotFound);
            return Task.CompletedTask;
        }
        public async Task ThereShouldBeNoItemsInTheCart(int orderId)
        {
            OrderDetail? result = await _unitOfWork.OrderDetailDal.GetAsync(b => b.OrderId == orderId);
            if (result != null) throw new BusinessException(ThereAreProductsInTheCart);
        }

        public async Task IsThereAnyProductInTheCart(int orderId)
        {
            OrderDetail? result = await _unitOfWork.OrderDetailDal.GetAsync(b => b.OrderId == orderId);
            if (result == null) throw new BusinessException(ThereAreNoItemsInTheCart);
        }

        public async Task<Product> TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(int productId,int quantity)
        {
            Product? result = await _unitOfWork.ProductDal.GetAsync(p => p.Id == productId);
            if (result.Quantity < quantity) throw new BusinessException(RequestedProductQuantityIsNotInStock);
            return result;
        }

        public async Task OperationMustBeAvailable()
        {
            List<OrderDetail>? results = _unitOfWork.OrderDetailDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(OrderDetailNotFound);
        }
        public IDataResult<List<OrderDetail>> MustBeARegisteredOrderDetail()
        {
            List<OrderDetail>? result = _unitOfWork.OrderDetailDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(OrderDetailNotFound);
            return new SuccessDataResult<List<OrderDetail>>(result, OrderDetailAvaliable);
        }

        public async Task<OrderDetail> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int id)
        {
            OrderDetail? result = await _unitOfWork.OrderDetailDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(OrderDetailNotFound);
            return result;
        }

    }
}
