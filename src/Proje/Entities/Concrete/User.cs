using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class User:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

        public User()
        {

        }

        public User(int id,string firstName, string lastName, string userName,
            string email, byte[] passwordHash, byte[] passwordSalt,DateTime registrationDate, bool status):this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            RegistrationDate = registrationDate;
            Status = status;
        }
    }
}
