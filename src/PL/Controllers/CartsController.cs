using BLL.DTOs.Cart;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<CartDto>> GetCart(int customerId)
        {
            var cart = await _cartService.GetCartAsync(customerId);
            return Ok(cart);
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddItem([FromBody] AddToCartDto dto)
        {
            await _cartService.AddItemAsync(dto.CustomerId, dto.ProductId, dto.Quantity);
            return NoContent();
        }

        [HttpDelete("clear-cart/{customerId:int}")]
        public async Task<IActionResult> ClearCart(int customerId)
        {
            await _cartService.ClearCartAsync(customerId);
            return NoContent();
        }
    }
}
