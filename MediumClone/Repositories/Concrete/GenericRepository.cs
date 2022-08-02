using MediumClone.Models.Context;
using MediumClone.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MediumClone.Repositories.Concrete
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext db;
        public GenericRepository(AppDbContext appDbContext)
        {
            this.db = appDbContext;
        }
        public bool Add(T entity)
        {
            try
            {
                db.Set<T>().Add(entity);
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw new Exception("Error: Somethings wrong in saving process...");
            }        
        }

        public bool Delete(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return db.Set<T>();
            }
            catch (Exception)
            {

                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }

        public T GetById(int id)
        {
            try
            {
                return db.Set<T>().Find(id);
            }
            catch (Exception)
            {

                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }      
        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return db.Set<T>().Where(predicate);
            }
            catch (Exception)
            {

                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return db.Set<T>().SingleOrDefault(predicate);
            }
            catch (Exception)
            {
                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }

        public bool Update(T entity)
        {
            try
            {
                db.Set<T>().Update(entity);
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }
    }
}
