using BLL.DTOs.Customer;
using Common.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System.Security.Cryptography;
using System.Text;
using BLL.Logging;

namespace BLL.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;
        public CustomerService(IUnitOfWork uow) => _uow = uow;

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _uow.Customers.GetByIdAsync(id);
            if (customer == null) throw new EntityNotFoundException(nameof(Customer), id);
            return new CustomerDto
            {
                Id = customer.Id, FullName = customer.FullName,
                Email = customer.Email, Address = customer.Address
            };
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
        {
            var existingCustomer = await _uow.Customers.FirstOrDefaultAsync(c => c.Email == dto.Email);
            if (existingCustomer != null)
            {
                Logger.Instance.LogWarning($"Registration failed: Email {dto.Email} already exists.");
                throw new InvalidOperationException("Email already exists");
            }

            var customer = new Customer
            {
                FullName = dto.FullName, Email = dto.Email,
                PasswordHash = HashPassword(dto.PasswordHash), Address = dto.Address
            };
            await _uow.Customers.AddAsync(customer);
            await _uow.SaveChangesAsync();
            
            Logger.Instance.LogInfo($"New customer registered successfully: {customer.Email} with ID {customer.Id}.");

            return new CustomerDto
            {
                Id = customer.Id, FullName = customer.FullName,
                Email = customer.Email, Address = customer.Address
            };
        }

        public async Task<CustomerDto?> LoginAsync(string email, string password)
        {
            var customer = await _uow.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (customer == null || customer.PasswordHash != HashPassword(password))
            {
                Logger.Instance.LogWarning($"Login failed for email: {email}. Invalid credentials.");
                return null;
            }

            Logger.Instance.LogInfo($"Customer logged in successfully: {customer.Email} with ID {customer.Id}.");

            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                Address = customer.Address
            };
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}