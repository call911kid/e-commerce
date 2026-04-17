using BLL.DTOs.Order;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(int customerId);
    }
}