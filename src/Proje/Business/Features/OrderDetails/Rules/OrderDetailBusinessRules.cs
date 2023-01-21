using static Business.Features.OrderDetails.Constants.OrderDetailMessages;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Features.OrderDetails.Rules
{
    public class OrderDetailBusinessRules:BaseBusinessRules
    {
        private readonly IOrderDetailDal _OrderDetailDal;
        private readonly IProductDal _productDal;

        public OrderDetailBusinessRules(IOrderDetailDal OrderDetailDal, IProductDal productDal)
        {
            _OrderDetailDal = OrderDetailDal;
            _productDal = productDal;
        }

        public async Task OrderDetailIdShouldExistWhenSelected(int? id)
        {
            OrderDetail? result = await _OrderDetailDal.GetAsync(b => b.Id == id);
            if (result ==null) throw new BusinessException(OrderDetailNotFound);
        }
        public async Task ThereShouldBeNoItemsInTheCart(int orderId)
        {
            OrderDetail? result = await _OrderDetailDal.GetAsync(b => b.OrderId == orderId);
            if (result != null) throw new BusinessException(ThereAreProductsInTheCart);
        }

        public async Task IsThereAnyProductInTheCart(int orderId)
        {
            OrderDetail? result = await _OrderDetailDal.GetAsync(b => b.OrderId == orderId);
            if (result == null) throw new BusinessException(ThereAreNoItemsInTheCart);
        }

        public async Task<Product> TheNumberOfProductsOrderDetailedShouldNotBeMoreThanStock(int productId,int quantity)
        {
            Product? result = await _productDal.GetAsync(p => p.Id == productId);
            if (result.Quantity < quantity) throw new BusinessException(RequestedProductQuantityIsNotInStock);
            return result;
        }

        public async Task OperationMustBeAvailable()
        {
            List<OrderDetail>? results = _OrderDetailDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(OrderDetailNotFound);
        }
        public IDataResult<List<OrderDetail>> MustBeARegisteredOrderDetail()
        {
            List<OrderDetail>? result = _OrderDetailDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(OrderDetailNotFound);
            return new SuccessDataResult<List<OrderDetail>>(result, OrderDetailAvaliable);
        }

        public async Task<OrderDetail> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int id)
        {
            OrderDetail? result = await _OrderDetailDal.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException(OrderDetailNotFound);
            return result;
        }

    }
}
