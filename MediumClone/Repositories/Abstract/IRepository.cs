using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MediumClone.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
    }
}
