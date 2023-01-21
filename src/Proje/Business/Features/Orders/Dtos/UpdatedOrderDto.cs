namespace Business.Features.Orders.Dtos
{
    public class UpdatedOrderDto
    {
        public int Id { get; set; }
        public int UserCartId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool Status { get; set; }
    }
}
