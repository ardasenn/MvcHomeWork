using Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediumClone.Models
{
    public class NewArticleVM
    {
        public NewArticleVM()
        {
            AllCategories = new HashSet<Category>();
            CategoriesArticle=new List<Category>();
        }
       
        public string Title { get; set; }
       
        public string Content { get; set; }
        
        public int[] CategoryIds { get; set; }

        public IEnumerable<Category> AllCategories { get; set; }
        public IEnumerable<Category> CategoriesArticle { get; set; }
        public int ArticleId { get; set; }
    }
}
