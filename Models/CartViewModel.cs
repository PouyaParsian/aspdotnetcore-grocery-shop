using System.Collections.Generic;
using System.Linq;

namespace MyEshop.Models
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; } = new();

        public decimal TotalOrder { get; set; }
    }
}
