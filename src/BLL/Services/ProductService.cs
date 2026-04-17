using BLL.DTOs.Product;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        public ProductService(IUnitOfWork uow) => _uow = uow;

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _uow.Products.GetByIdAsync(id);
            if (product == null) throw new EntityNotFoundException(nameof(Product), id);
            return new ProductDto
            {
                Id = product.Id, Name = product.Name, Description = product.Description,
                Price = product.Price, StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl, CategoryId = product.CategoryId
            };
        }

        public async Task UpdateStockAsync(int productId, int quantityToDeduct)
        {
            var product = await _uow.Products.GetByIdAsync(productId);
            if (product == null) throw new EntityNotFoundException(nameof(Product), productId);
            if (product.StockQuantity < quantityToDeduct) throw new InsufficientStockException(product.Name, quantityToDeduct, product.StockQuantity);
            product.StockQuantity -= quantityToDeduct;
            _uow.Products.Update(product);
            await _uow.SaveChangesAsync();
        }
    }
}