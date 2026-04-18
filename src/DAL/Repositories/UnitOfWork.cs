using DAL.Context;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ICartRepository Cart { get; private set; }
        public ICartItemRepository CartItems { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }
        public UnitOfWork(ApplicationDbContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository)
        {
            _context = context;
            Products = productRepository;
            Categories = categoryRepository;
            Cart = cartRepository;
            CartItems = cartItemRepository;
            Customers = customerRepository;
            Orders = orderRepository;
            OrderItems = orderItemRepository;
        }

        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync() =>
            await _context.Database.BeginTransactionAsync();

        public void Dispose() => _context.Dispose();
    }
}
