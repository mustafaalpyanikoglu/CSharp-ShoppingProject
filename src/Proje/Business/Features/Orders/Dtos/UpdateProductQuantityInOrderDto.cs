namespace Business.Features.Orders.Dtos
{
    public class UpdateProductQuantityInOrderDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
