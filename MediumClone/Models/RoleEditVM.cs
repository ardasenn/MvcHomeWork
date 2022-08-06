using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MediumClone.Models
{
    public class RoleEditVM
    {

        public IdentityRole Role { get; set; }

        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}
