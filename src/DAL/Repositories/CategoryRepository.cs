using DAL.Context;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
