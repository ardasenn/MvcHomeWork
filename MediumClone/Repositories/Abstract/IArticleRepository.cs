using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MediumClone.Repositories.Abstract
{
    public interface IArticleRepository: IRepository<Article>
    {
        IEnumerable<Article> GetAllIncludeAuthors();
        IEnumerable<Article> GetTrendingArticles(int count);
        IEnumerable<Article> GetAllArtricleByInterestedIn(List<int> list);
        IEnumerable<Article> GetTop10Articles();
        Article GetArticleByIdWithCategories(int id);
        IEnumerable<Article> GetAllArticlesByAuthor(string id);
        Article GetArticleWithCategoriesAndAuthor(int id);
        IEnumerable<Article> GetAllArticleWithCategoriesAndAuthor();
    }
}
