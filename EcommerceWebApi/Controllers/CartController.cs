using Ecommerce.Models.DTO;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        // POST: Cart
        [HttpPost("{id}")]
        public IActionResult AddToCart(string id, CartItem cartItem)
        {
            // Retrieve the cartId from session
            //For some reason this is always null or empty so pass value from FE as a fallback
            var cartId = GetCookie("CartId");

            if (string.IsNullOrWhiteSpace(cartId) && string.IsNullOrWhiteSpace(id))
            {
                cartId = $"Cart-{Guid.NewGuid()}";
            }

            // id from FE
            if (!string.IsNullOrWhiteSpace(id))
            {
                cartId = id;
            }
  
            _cartService.AddToCart(cartId, cartItem);

            SetCookie("CartId", cartId);

            _logger.LogInformation($"{cartId} cart created.");
            return Created($"/Cart/{cartId}", cartId);
        }

        // GET: Cart/id
        [HttpGet("{id}")]
        public IActionResult Detail(string id)
        {
            var cart = _cartService.GetCartById(id);
            return Ok(cart);
        }

        //PUT: Cart/id
        [HttpPut("{id}")]
        public IActionResult UpdateCart(string id, CartItem cartItem)
        {
            _cartService.UpdateCart(id, cartItem);
            _logger.LogInformation($"Cart {id} is created.");
            return Ok(new { message = $"Cart {id} is updated." });
        }

        //DELETE: Cart/id
        [HttpDelete("{id}")]
        public IActionResult DeleteCart(string id)
        {
            _cartService.DeleteCart(id);
            _logger.LogInformation($"Cart {id} is deleted.");
            return Ok(new { message = $"Cart {id} is deleted." });
        }

        //DELETE: Cart/id/productid
        [HttpDelete("{id}/{productid}")]
        public IActionResult DeleteCartItem(string id, int productId)
        {
            var cart = _cartService.DeleteCartItem(id, productId);
            _logger.LogInformation($"Product {productId} is deleted.");
            return Ok(cart);
        }

        private void SetCookie(string key, string value)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(1)
            };

            Response.Cookies.Append(key, value, cookieOptions);

        }

        private string GetCookie(string key)
        {
            string value = Request.Cookies[key];
            return value != null ? value : string.Empty;
        }
    }
}
