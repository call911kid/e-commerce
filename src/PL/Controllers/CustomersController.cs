using BLL.DTOs.Customer;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
        {
            var customer = await _customerService.CreateAsync(dto);
            return Ok();
        }
    }
}
