﻿namespace Business.Features.UserCarts.Dtos
{
    public class UserCartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}