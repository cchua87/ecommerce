using Ecommerce.Entities;
using Ecommerce.Models.DTO;

namespace Ecommerce.Services
{
    public interface ICartService
    {
        Cart GetCartById(string id);
        void AddToCart(string id, CartItem model);
        void UpdateCart(string id, CartItem model);
        void DeleteCart(string id);
        Cart DeleteCartItem(string id, int productId);
    }
}
