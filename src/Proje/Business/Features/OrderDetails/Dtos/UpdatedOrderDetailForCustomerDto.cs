namespace Business.Features.OrderDetails.Dtos
{
    public class UpdatedOrderDetailForCustomerDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}
