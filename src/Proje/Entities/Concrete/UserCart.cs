using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class UserCart:Entity
    {
        public int UserId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public User User { get; set; }

        public UserCart()
        {
            Orders = new HashSet<Order>();
        }
        public UserCart(int id, int userId):this()
        {
            Id= id; 
            UserId= userId;
        }
    }
}
