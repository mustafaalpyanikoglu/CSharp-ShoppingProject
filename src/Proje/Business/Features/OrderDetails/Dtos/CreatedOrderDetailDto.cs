namespace Business.Features.OrderDetails.Dtos
{
    public class CreatedOrderDetailDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}
