using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class Purse:Entity
    {
        public int UserId { get; set; }
        public int Money { get; set; }

        public virtual User User { get; set; }

        public Purse()
        {

        }

        public Purse(int id, int userId, int money):this()
        {
            Id= id;
            UserId= userId;
            Money= money;
        }
    }
}
