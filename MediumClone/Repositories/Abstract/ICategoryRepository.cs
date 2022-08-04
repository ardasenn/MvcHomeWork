using Entities;
using System.Collections.Generic;

namespace MediumClone.Repositories.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesById(string id);
    }
}
