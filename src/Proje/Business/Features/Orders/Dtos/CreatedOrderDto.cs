namespace Business.Features.Orders.Dtos
{
    public class CreatedOrderDto
    {
        public int UserCartId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public float OrderAmount { get; set; }
    }
}
