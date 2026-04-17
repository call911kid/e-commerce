using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository Cart { get; }
        ICartItemRepository CartItems { get; }
        ICategoryRepository Categories { get; }
        ICustomerRepository Customers { get; }
        IOrderItemRepository OrderItems { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
