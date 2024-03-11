using Ecommerce.Models.DTO;

namespace Ecommerce.Entities
{
    public class CartEntity
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public string CartItems { get; set; }
    }

    public class CartItemEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
