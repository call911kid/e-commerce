using BLL.DTOs.Order;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
        {
            var order = await _orderService.CreateOrderAsync(dto.CustomerId);
            return Ok(order);
        }
    }
}
