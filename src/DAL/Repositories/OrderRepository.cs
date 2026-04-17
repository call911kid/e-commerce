using DAL.Context;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
