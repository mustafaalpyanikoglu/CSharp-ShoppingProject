﻿namespace Business.Features.Orders.Dtos
{
    public class ConfirmOrderDto
    {
        public string OrderNumber { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
