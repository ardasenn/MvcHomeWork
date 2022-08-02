using Entities;
using MediumClone.Models.Context;
using MediumClone.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MediumClone.Repositories.Concrete
{
    public class ArticleRepository : GenericRepository<Article> ,IArticleRepository
    {
        private readonly AppDbContext db;

        public ArticleRepository(AppDbContext db) : base(db)
        {
            this.db = db;
        }

        public IEnumerable<Article> GetAllIncludeAuthors()
        {
            try
            {
                return db.Articles.Include(a => a.Author);
            }
            catch (Exception)
            {
                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }
    }
}
