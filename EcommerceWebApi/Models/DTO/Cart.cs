using Ecommerce.Entities;

namespace Ecommerce.Models.DTO
{
    public class Cart
    {
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
