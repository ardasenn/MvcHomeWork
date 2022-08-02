using MediumClone.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
        public Category()
        {
            Articles=new HashSet<Article>();
            AppUsers = new HashSet<AppUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
