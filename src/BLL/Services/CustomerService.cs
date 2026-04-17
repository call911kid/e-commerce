using BLL.DTOs.Customer;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
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
            var customer = new Customer
            {
                FullName = dto.FullName, Email = dto.Email,
                PasswordHash = dto.PasswordHash, Address = dto.Address
            };
            await _uow.Customers.AddAsync(customer);
            await _uow.SaveChangesAsync();
            return new CustomerDto
            {
                Id = customer.Id, FullName = customer.FullName,
                Email = customer.Email, Address = customer.Address
            };
        }
    }
}