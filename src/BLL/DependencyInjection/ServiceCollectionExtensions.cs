using BLL.Interfaces;
using BLL.Services;
using BLL.Strategies.Discount;
using BLL.Strategies.Payment;
using BLL.Strategies.Shipping;
using BLL.Strategies.Notification;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();

            // Strategies
            services.AddScoped<IDiscountStrategy>(_ => new PercentageDiscount(10));
            services.AddScoped<IShippingStrategy, StandardShipping>();
            services.AddScoped<IPaymentStrategy, CreditCardPayment>();
            services.AddScoped<INotificationStrategy, EmailNotification>();

            return services;
        }
    }
}