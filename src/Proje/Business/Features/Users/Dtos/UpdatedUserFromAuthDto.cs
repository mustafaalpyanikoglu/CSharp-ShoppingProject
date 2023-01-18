using Core.Utilities.Security.Jwt;

namespace Business.Features.Users.Dtos
{
    public class UpdatedUserFromAuthDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}
