using Ecommerce.Entities;
using Ecommerce.Models.DTO;

namespace Ecommerce.Repositories
{
    public interface ICartRepository
    {
        CartEntity? GetCartById(string cartId);
        void AddCart(CartEntity cart);
        void UpdateCart(CartEntity cart);
        void DeleteCart(CartEntity cart);
        void AddCartItem(CartItemEntity cartItem);
        void DetachFromContext(CartEntity cart);
    }
}
