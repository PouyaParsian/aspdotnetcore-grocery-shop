using System.Collections.Generic;
using System.Linq;

namespace MyEshop.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<CartItem> CartItems { get; set; } = new();

        public void AddItem(CartItem item)
        {
            var existing = CartItems.FirstOrDefault(c => c.ItemId == item.ItemId);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                CartItems.Add(item);
            }
        }

        public void RemoveItem(int itemId, bool removeAll = false)
        {
            var existing = CartItems.FirstOrDefault(c => c.ItemId == itemId);

            if (existing == null)
                return;

            if (removeAll || existing.Quantity <= 1)
                CartItems.Remove(existing);
            else
                existing.Quantity--;
        }

        public decimal GetTotalPrice()
        {
            return CartItems.Sum(i => i.GetTotalPrice());
        }
    }
}