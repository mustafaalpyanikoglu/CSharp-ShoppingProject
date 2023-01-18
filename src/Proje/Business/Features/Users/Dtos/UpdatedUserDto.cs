namespace Business.Features.Users.Dtos
{
    public class UpdatedUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
    }
}
