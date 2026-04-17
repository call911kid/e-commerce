using BLL.DTOs.Customer;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CreateCustomerDto dto);
        Task<CustomerDto?> LoginAsync(string email, string password);
    }
}