using Entities;
using MediumClone.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;

namespace MediumClone.Models.Authentication
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string UserDescription { get; set; }       
        public ICollection<Category> Categories { get; set; }        
        public ProfileImage ProfileImage { get; set; }

        public AppUser()
        {
            Articles = new HashSet<Article>();
            Categories = new HashSet<Category>();           
        }
        public ICollection<Article> Articles { get; set; }
    }
}
