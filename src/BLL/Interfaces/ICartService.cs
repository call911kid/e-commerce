using BLL.DTOs.Cart;

namespace BLL.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int customerId);
        Task AddItemAsync(int customerId, int productId, int quantity);
        Task ClearCartAsync(int customerId);
    }
}