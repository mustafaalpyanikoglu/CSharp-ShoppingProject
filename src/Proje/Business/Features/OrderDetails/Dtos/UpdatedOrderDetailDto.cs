namespace Business.Features.OrderDetails.Dtos
{
    public class UpdatedOrderDetailDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}
