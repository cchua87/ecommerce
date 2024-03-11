using AutoMapper;
using Ecommerce.Data;
using Ecommerce.Entities;
using Ecommerce.Models.DTO;
using Ecommerce.Repositories;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Services
{
    public class CartService : ICartService
    {

        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productsService;
        private readonly ILogger<ProductService> _logger;
        public CartService(ICartRepository cartRepository, IMapper mapper, IProductService productsService, ILogger<ProductService> logger)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productsService = productsService;
            _logger = logger;
        }
        public void AddToCart(string cartId, CartItem model)
        {
            try
            {
                //get product details and calculate line total
                var product = _productsService.GetProductById(model.ProductId);
                var price = product.Price;
                model.LineTotal = model.Quantity * price;
                model.ProductName = product.Name;

                var cartEntity = getCart(cartId);
                if (cartEntity != null)
                {
                    // detach from context
                    _cartRepository.DetachFromContext(cartEntity);
                    // update cart
                    updateCart(cartId, cartEntity, model, price);
                }
                else
                {
                    //add cart item
                    var cartItem = _mapper.Map<CartItemEntity>(model);
                    _cartRepository.AddCartItem(cartItem);

                    //add cart
                    var cartItemList = new List<CartItemEntity>
                    {
                        cartItem
                    };
                    var cart = new CartEntity
                    {
                        CartId = cartId,
                        CartItems = JsonConvert.SerializeObject(cartItemList)
                    };
                    _cartRepository.AddCart(cart);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"AddToCart failed: {ex}");
            }
        }

        public void DeleteCart(string id)
        {
            try
            {
                var cart = getCart(id);
                _cartRepository.DeleteCart(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteCart failed: {ex}");
            }
        }

        public Cart DeleteCartItem(string id, int productId)
        {
            var cart = new Cart();
            try
            {
                var cartEntity = getCart(id);
                var cartItems = new List<CartItemEntity>();
                cartItems = JsonConvert.DeserializeObject<List<CartItemEntity>>(cartEntity.CartItems);
                cartItems = cartItems?.Where(item => item.ProductId != productId).ToList();

                // detach from context
                _cartRepository.DetachFromContext(cartEntity);

                var cartItemJson = JsonConvert.SerializeObject(cartItems);
                var updatedCartEntity = new CartEntity
                {
                    CartId = id,
                    CartItems = cartItemJson,
                    Id = cartEntity.Id
                };

                _cartRepository.UpdateCart(updatedCartEntity);
                cart = GetCartById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateCart failed: {ex}");
            }
            return cart;
        }

        public Cart GetCartById(string id)
        {
            var cartEntity = getCart(id);
            var cartItems = new List<CartItemEntity>();
            var cart = new Cart();
            if (cartEntity != null)
            {
                cartItems = JsonConvert.DeserializeObject<List<CartItemEntity>>(cartEntity.CartItems);
                cart.CartItems = _mapper.Map<List<CartItem>>(cartItems);
                cart.CartId = id;
                cart.SubTotal = cart.CartItems.Select(x => x.LineTotal).Sum();
            }
            return cart;
        }

        public void UpdateCart(string id, CartItem model)
        {
            try
            {
                //get product details and calculate line total
                var product = _productsService.GetProductById(model.ProductId);
                var price = product.Price;
                model.LineTotal = model.Quantity * price;
                model.ProductName = product.Name;

                var cartEntity = getCart(id);
                updateCart(id, cartEntity, model, price);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateCart failed: {ex}");
            }
        }

        private CartEntity getCart(string id)
        {
            return _cartRepository.GetCartById(id);
        }

        private void updateCart(string cartId, CartEntity cartEntity, CartItem model, decimal price)
        {
            //validate if product id already exists in the cart then update cart
            var cartItems = new List<CartItemEntity>();
            cartItems = JsonConvert.DeserializeObject<List<CartItemEntity>>(cartEntity.CartItems);
            var existingItem = cartItems?.FirstOrDefault(item => item.ProductId == model.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += model.Quantity;
                existingItem.LineTotal = existingItem.Quantity * price;
                existingItem.ProductName = model.ProductName;
            }
            else
            {
                //add cart item to the existing cart
                var cartItem = _mapper.Map<CartItemEntity>(model);
                _cartRepository.AddCartItem(cartItem);

                cartItems?.Add(new CartItemEntity
                {
                    Id = cartItem.Id,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    Quantity = model.Quantity,
                    LineTotal = model.LineTotal,
                });
            }
            var cartItemJson = JsonConvert.SerializeObject(cartItems);
            var cart = new CartEntity
            {
                CartId = cartId,
                CartItems = cartItemJson,
                Id = cartEntity.Id
            };

            _cartRepository.UpdateCart(cart);
        }
    }
}
