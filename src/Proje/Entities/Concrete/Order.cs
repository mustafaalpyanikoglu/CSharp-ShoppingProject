using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class Order:Entity
    {
        public int UserCartId { get; set; }
        public int ProductId { get; set; }
        public string OrderNumber { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool Status { get; set; }

        public virtual UserCart UserCart { get; set; }
        public virtual Product Product { get; set; }

        public Order()
        {

        }

        public Order(int id, int userCartId, int productId,
            string orderNumber, int quantity, float totalPrice, DateTime orderDate, DateTime approvalDate,  bool status) : this()
        {
            Id = id;
            UserCartId = userCartId;
            ProductId = productId;
            OrderNumber = orderNumber;
            Quantity = quantity;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
            ApprovalDate = approvalDate;
            Status = status;
        }
    }
}
