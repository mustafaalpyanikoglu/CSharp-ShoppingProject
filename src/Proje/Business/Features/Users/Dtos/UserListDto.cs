﻿namespace Business.Features.Users.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
