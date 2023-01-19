﻿using Core.Persistence.Repositories;

namespace Entities.Concrete
{
    public class Product:Entity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public Product(int id, int categoryId, string name, int quantity, float price):this()
        {
            Id= id;
            CategoryId= categoryId;
            Name= name;
            Quantity= quantity;
            Price= price;
        }
    }
}
