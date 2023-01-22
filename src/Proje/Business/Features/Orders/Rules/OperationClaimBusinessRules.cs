using static Business.Features.Orders.Constants.OrderMessages;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Entities.Concrete;
using DataAccess.Concrete.Contexts;

namespace Business.Features.Orders.Rules
{
    public class OrderBusinessRules:BaseBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OrderIdShouldExistWhenSelected(int? id)
        {
            Order? result = await _unitOfWork.OrderDal.GetAsync(b => b.Id == id);
            if (result ==null) throw new BusinessException(OrderNotFound);
        }
        public async Task OrderNumberShouldExistWhenSelected(string? orderNumber)
        {
            Order? result = await _unitOfWork.OrderDal.GetAsync(b => b.OrderNumber == orderNumber);
            if (result == null) throw new BusinessException(OrderNotFound);
        }

        public Task OrderStatusMustBeFalse(bool status)
        {
            if (status == true) throw new BusinessException(OrderHasAlreadyBeenConfirmed);
            return Task.CompletedTask;
        }

        public async Task OrderNumberMustBeUnique(string orderNumber)
        {
            Order? result = await _unitOfWork.OrderDal.GetAsync(p => p.OrderNumber == orderNumber);
            if (result != null) throw new BusinessException(OrderNumberIsNotUnique);
        }
        public async Task<Order> HasAnOrderBeenCreated(Order order)
        {
            Order? result = await _unitOfWork.OrderDal.GetAsync(p => p.OrderNumber == order.OrderNumber);
            if (result != null) throw new BusinessException(OrderHasALreadyBeenCreated);
            return result;
        }

        public async Task OperationMustBeAvailable()
        {
            List<Order>? results = _unitOfWork.OrderDal.GetAll();
            if (results.Count <= 0) throw new BusinessException(OrderNotFound);
        }
        public IDataResult<List<Order>> MustBeARegisteredOrder()
        {
            List<Order>? result = _unitOfWork.OrderDal.GetAll();
            if (result.Count <= 0) throw new BusinessException(OrderNotFound);
            return new SuccessDataResult<List<Order>>(result, OrderAvaliable);
        }

        public async Task<Order> ExistingDataShouldBeFetchedWhenTransactionRequestIdIsSelected(int orderID)
        {
            Order? result = await _unitOfWork.OrderDal.GetAsync(b => b.Id == orderID);
            if (result == null) throw new BusinessException(OrderNotFound);
            return result;
        }
        public async Task<Order> IsThereAnOrderToBeConfirmed(int userCartId)
        {
            Order? result = await _unitOfWork.OrderDal.GetAsync(b => b.UserCartId == userCartId && b.Status == false);
            if (result == null) throw new BusinessException(OrderNotFound);
            return result;
        }
    }
}
