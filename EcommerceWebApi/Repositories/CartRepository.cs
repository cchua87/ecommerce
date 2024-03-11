using Ecommerce.Data;
using Ecommerce.Entities;

namespace Ecommerce.Repositories
{
    public class CartRepository : ICartRepository
    {
        private DataContext _context;
        public CartRepository(DataContext context)
        {
            _context = context;
        }
        public void AddCart(CartEntity cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void AddCartItem(CartItemEntity cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void DeleteCart(CartEntity cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public void DetachFromContext(CartEntity cart)
        {
            _context.Entry(cart).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

        public CartEntity? GetCartById(string cartId)
        {
            return _context.Carts.Where(x => x.CartId.Equals(cartId)).FirstOrDefault();
        }

        public void UpdateCart(CartEntity cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges(true);
        }
    }
}
