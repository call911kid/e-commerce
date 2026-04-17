using DAL.Context;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
