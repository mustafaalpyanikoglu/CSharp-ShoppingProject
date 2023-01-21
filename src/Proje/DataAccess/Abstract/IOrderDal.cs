using Core.Persistence.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IRepository<Order>, IAsyncRepository<Order>
    {
        //List<OrderListDtoForClient> GetListOrderByUserCart(int userCartId);
    }
}
