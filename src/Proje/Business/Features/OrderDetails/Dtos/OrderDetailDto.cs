namespace Business.Features.OrderDetails.Dtos
{
    public class OrderDetailDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrderNumber { get; set; }
        public int Quantity { get; set; }
        public float ProductTotalPrice { get; set; }
        public float OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool OrderStatus { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
    }
}
