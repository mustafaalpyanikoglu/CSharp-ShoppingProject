using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class Purse:Entity
    {
        public int UserId { get; set; }
        public float Money { get; set; }

        public virtual User User { get; set; }

        public Purse()
        {

        }

        public Purse(int id, int userId, float money):this()
        {
            Id= id;
            UserId= userId;
            Money= money;
        }
    }
}
