namespace Business.Features.Orders.Dtos
{
    public class ConfirmOrderDto
    {
        public string OrderNumber { get; set; }
        public float OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
