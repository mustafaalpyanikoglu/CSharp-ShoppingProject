using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class User:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Status { get; set; }


        public virtual ICollection<UserCart> UserCart { get; set; }
        public virtual ICollection<Purse> Purse { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }


        public User()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
            UserCart = new HashSet<UserCart>();
            Purse= new HashSet<Purse>();
        }

        public User(int id,string firstName, string lastName, string phoneNumber, string address,
            string email, byte[] passwordHash, byte[] passwordSalt,DateTime registrationDate, bool status):this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            RegistrationDate = registrationDate;
            Status = status;
        }
    }
}
