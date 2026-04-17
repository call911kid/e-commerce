using BLL.DTOs.Cart;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System.Linq;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _uow;
        public CartService(IUnitOfWork uow) => _uow = uow;

        public async Task<CartDto> GetCartAsync(int customerId)
        {
            var carts = await _uow.Cart.FindAsync(c => c.CustomerId == customerId);
            var cart = carts.FirstOrDefault();
            if (cart == null) return new CartDto { CustomerId = customerId };

            return new CartDto
            {
                Id = cart.Id,
                CustomerId = cart.CustomerId,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task AddItemAsync(int customerId, int productId, int quantity)
        {
            var product = await _uow.Products.GetByIdAsync(productId);
            if (product == null) throw new EntityNotFoundException(nameof(Product), productId);
            if (product.StockQuantity < quantity) throw new InsufficientStockException(product.Name, quantity, product.StockQuantity);

            var carts = await _uow.Cart.FindAsync(c => c.CustomerId == customerId);
            var cart = carts.FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart { CustomerId = customerId };
                await _uow.Cart.AddAsync(cart);
                await _uow.Cart.SaveChangesAsync();
            }

            var items = await _uow.CartItems.FindAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);
            var item = items.FirstOrDefault();

            if (item != null)
            {
                item.Quantity += quantity;
                _uow.CartItems.Update(item);
            }
            else
            {
                await _uow.CartItems.AddAsync(new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }
            await _uow.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int customerId)
        {
            var carts = await _uow.Cart.FindAsync(c => c.CustomerId == customerId);
            var cart = carts.FirstOrDefault();
            if (cart == null) return;

            var items = await _uow.CartItems.FindAsync(ci => ci.CartId == cart.Id);
            foreach (var item in items) _uow.CartItems.Delete(item);

            _uow.Cart.Delete(cart);
            await _uow.SaveChangesAsync();
        }
    }
}