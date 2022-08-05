using Entities;
using System.Collections.Generic;

namespace MediumClone.Models
{
    public class AuthorsArticlesVM
    {

        public AuthorsArticlesVM()
        {
            Articles = new List<Article>();
            Categories = new List<Category>();
        }
        public string UserId { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
