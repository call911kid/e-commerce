using BLL.DTOs.Product;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task UpdateStockAsync(int productId, int quantityToDeduct);
    }
}