namespace MyEshop.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public decimal GetTotalPrice()
        {
            return Item.Price * Quantity;
        }
    }
}
