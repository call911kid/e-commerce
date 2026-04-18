using BLL.DTOs.Cart;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Responses;

namespace PL.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<ApiResponse<CartDto>>> GetCart(int customerId)
        {
            var cart = await _cartService.GetCartAsync(customerId);
            return Ok(ApiResponse.Success(cart));
        }

        [HttpPost("add-to-cart")]
        public async Task<ActionResult<ApiResponse>> AddItem([FromBody] AddToCartDto dto)
        {
            await _cartService.AddItemAsync(dto.CustomerId, dto.ProductId, dto.Quantity);
            return Ok(ApiResponse.Success());
        }

        [HttpDelete("clear-cart/{customerId:int}")]
        public async Task<ActionResult<ApiResponse>> ClearCart(int customerId)
        {
            await _cartService.ClearCartAsync(customerId);
            return Ok(ApiResponse.Success());
        }
    }
}
