using DAL.Context;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
