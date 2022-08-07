using Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MediumClone.Models
{
    public class UserVM
    {
        public UserVM()
        {
            Categories=new HashSet<Category>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string UserName { get; set; }       
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Category> Categories { get; set; }
        //bir ara Enumarab ile Colleciton fakrına bakılacak.
    }
}
