using BLL.DTOs.Product;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Responses;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(ApiResponse.Success(product));
        }
    }
}
