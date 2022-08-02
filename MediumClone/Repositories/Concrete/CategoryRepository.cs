using Entities;
using MediumClone.Models.Context;
using MediumClone.Repositories.Abstract;

namespace MediumClone.Repositories.Concrete
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        private readonly AppDbContext db;

        public CategoryRepository(AppDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
