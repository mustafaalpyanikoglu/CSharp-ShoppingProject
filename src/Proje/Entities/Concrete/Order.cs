using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class Order:Entity
    {
        public int UserCartId { get; set; }
        public string OrderNumber { get; set; }
        public float OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool Status { get; set; }

        public virtual UserCart UserCart { get; set; }

        public Order()
        {
        }

        public Order(int id, int userCartId,string orderNumber,
            DateTime orderDate, DateTime approvalDate,float orderAmount,  bool status) : this()
        {
            Id = id;
            UserCartId = userCartId;
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            ApprovalDate = approvalDate;
            OrderAmount = orderAmount;
            Status = status;
        }
    }
}
