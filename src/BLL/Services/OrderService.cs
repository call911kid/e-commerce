using BLL.DTOs.Order;
using Common.Exceptions;
using BLL.Interfaces;
using BLL.Logging;
using BLL.Strategies.Discount;
using BLL.Strategies.Payment;
using BLL.Strategies.Shipping;
using BLL.Strategies.Notification;
using DAL.Interfaces;
using DAL.Models;
using System.Linq;

namespace BLL.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICartService _cartService;
        private readonly IDiscountStrategy _discountStrategy;
        private readonly IShippingStrategy _shippingStrategy;
        private readonly IPaymentStrategy _paymentStrategy;
        private readonly INotificationStrategy _notificationStrategy;
        private readonly Logger _logger;

        public OrderService(IUnitOfWork uow, ICartService cartService,
                            IDiscountStrategy discountStrategy,
                            IShippingStrategy shippingStrategy,
                            IPaymentStrategy paymentStrategy,
                            INotificationStrategy notificationStrategy)
        {
            _uow = uow; _cartService = cartService;
            _discountStrategy = discountStrategy; _shippingStrategy = shippingStrategy;
            _paymentStrategy = paymentStrategy; _notificationStrategy = notificationStrategy;
            _logger = Logger.Instance;
        }

        public async Task<OrderDto> CreateOrderAsync(int customerId)
        {
            _logger.LogOrderStarted(customerId);

            var cartDto = await _cartService.GetCartAsync(customerId);
            if (!cartDto.Items.Any()) throw new InvalidCartException("Cart is empty.");

            decimal subTotal = cartDto.SubTotal;
            decimal discount = _discountStrategy.Apply(subTotal);
            decimal afterDiscount = subTotal - discount;
            decimal tax = afterDiscount * 0.14m;
            decimal shipping = _shippingStrategy.Calculate(afterDiscount);
            decimal total = afterDiscount + tax + shipping;

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                SubTotal = subTotal,
                DiscountAmount = discount,
                TaxAmount = tax,
                TotalAmount = total
            };

            await _uow.Orders.AddAsync(order);
            await _uow.SaveChangesAsync();

            foreach (var itemDto in cartDto.Items)
            {
                var product = await _uow.Products.GetByIdAsync(itemDto.ProductId);
                if (product.StockQuantity < itemDto.Quantity)
                    throw new InsufficientStockException(product.Name, itemDto.Quantity, product.StockQuantity);

                product.StockQuantity -= itemDto.Quantity;
                _uow.Products.Update(product);

                await _uow.OrderItems.AddAsync(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = itemDto.ProductId,
                    ProductName = product.Name,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice
                });
            }

            bool paymentSuccess = await _paymentStrategy.ProcessAsync(total);
            if (!paymentSuccess) throw new OrderProcessingException("Payment failed.");

            _logger.LogPaymentProcessed(total);

            _notificationStrategy.Send(customerId, $"Order #{order.Id} completed. Total: {total:C}");

            await _cartService.ClearCartAsync(customerId);
            await _uow.SaveChangesAsync();

            _logger.LogOrderCompleted(order.Id);

            return new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                SubTotal = order.SubTotal,
                DiscountAmount = order.DiscountAmount,
                TaxAmount = order.TaxAmount,
                TotalAmount = order.TotalAmount,
                Items = cartDto.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }



   
    }
}