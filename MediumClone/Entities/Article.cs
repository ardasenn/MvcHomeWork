using MediumClone.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Article
    {
        public Article()
        {
            Categories=new HashSet<Category>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;        
        public ICollection<Category> Categories { get; set; }   
        public AppUser Author { get; set; }
        public int ViewsCount { get; set; }
        public int ReadTime { get; set; }

    }
}
