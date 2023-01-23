﻿namespace Business.Features.OrderDetails.Dtos
{
    public class OrderDetailListDtoForCustomer
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public string OrderNumber { get; set; }
        public float OrderAmount { get; set; }
        public float ProductTotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
