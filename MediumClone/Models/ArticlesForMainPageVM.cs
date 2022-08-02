using Entities;
using System.Collections.Generic;

namespace MediumClone.Models
{
    public class ArticlesForMainPageVM
    {
        public ArticlesForMainPageVM()
        {
            TopViewedArticles = new List<Article>();
            Articles = new List<Article>();
        }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Article> TopViewedArticles { get; set; }
    }
}
