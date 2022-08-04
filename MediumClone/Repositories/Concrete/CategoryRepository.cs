using Entities;
using MediumClone.Models.Context;
using MediumClone.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MediumClone.Repositories.Concrete
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        private readonly AppDbContext db;

        public CategoryRepository(AppDbContext db) : base(db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetCategoriesById(string id)
        {
            return db.Categories.Include(a=>a.AppUsers).Where(a=>a.AppUsers.Any(a=>a.Id == id));
        }
          
    }
}
