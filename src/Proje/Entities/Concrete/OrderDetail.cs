using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class OrderDetail:Entity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public OrderDetail()
        {

        }

        public OrderDetail(int id, int orderId, int productId, int quantity, float totalPrice):this()
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }
    }
}
