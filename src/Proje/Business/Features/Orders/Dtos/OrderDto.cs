﻿namespace Business.Features.Orders.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserCartId { get; set; }
        public int ProductId { get; set; }
        public string OrderNumber { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool Status { get; set; }
    }
}
