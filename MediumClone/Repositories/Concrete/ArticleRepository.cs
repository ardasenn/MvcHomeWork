﻿using Entities;
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

        public IEnumerable<Article> GetAllArtricleByInterestedIn(List<int> list)
        {
            try
            {
                return db.Articles.Where(p => p.Categories.Any(l => list.Contains(l.Id)));
            }
            catch (Exception)
            {

                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }        

        public IEnumerable<Article> GetAllIncludeAuthors()
        {
            try
            {
                return db.Articles.Include(a => a.Author).OrderByDescending(a=>a.CreatedTime);//Eager Loading
            }
            catch (Exception)
            {
                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }

        public IEnumerable<Article> GetTop10Articles()
        {
            try
            {
                return db.Articles.Include(a => a.Author).OrderByDescending(a => a.ViewsCount).Take(3);//Eager Loading
            }
            catch (Exception)
            {
                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }

        public IEnumerable<Article> GetTrendingArticles(int count)
        {
            try
            {
                return db.Articles.Include(a => a.Author).Where(a => a.ViewsCount > count);

            }
            catch (Exception)
            {
                throw new Exception("Error: Somethings wrong in saving process...");
            }
        }
    }
}
