namespace Business.Features.OrderDetails.Dtos
{
    public class OrderDetailDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool OrderStatus { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
        public int OrderDetailQuantity { get; set; }
        public float OrderDetailTotalPrice { get; set; }
    }
}
