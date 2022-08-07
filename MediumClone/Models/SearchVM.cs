using Entities;
using System.Collections.Generic;

namespace MediumClone.Models
{
    public class SearchVM
    {
        public SearchVM()
        {
            Articles = new List<Article>();
            AllCategories = new List<Category>();
        }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Category> AllCategories { get; set; }
    }
}
