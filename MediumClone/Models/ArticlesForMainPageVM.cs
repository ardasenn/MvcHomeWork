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
            InterestedArticles = new List<Article>();
            Categories = new List<Category>();
        }
        public string Id { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Article> TopViewedArticles { get; set; }
        public IEnumerable<Article> InterestedArticles { get; set; }
        public int CategoryID { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
