using Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediumClone.Models
{
    public class NewArticleVM
    {
        public NewArticleVM()
        {
            Categories = new List<Category>();
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int[] CategoryIds { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
