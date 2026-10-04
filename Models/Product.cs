using System.Collections.Generic;

namespace MyEshop.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<Category> Categories { get; set; } = new();
        public List<Item> Items { get; set; } = new();
        
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
