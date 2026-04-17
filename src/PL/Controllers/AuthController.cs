using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BLL.Interfaces;
using BLL.DTOs.Customer;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public AuthController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var customer = await _customerService.LoginAsync(dto.Email, dto.Password);
            if (customer == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.FullName ?? string.Empty),
                new Claim(ClaimTypes.Email, customer.Email ?? string.Empty)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok(customer);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerDto dto)
        {
            try
            {
                var customer = await _customerService.CreateAsync(dto);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
