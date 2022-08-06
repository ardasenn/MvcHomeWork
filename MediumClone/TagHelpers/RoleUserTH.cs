using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediumClone.TagHelpers
{
    [HtmlTargetElement("td",Attributes ="i-role")]
    public class RoleUserTH :TagHelper
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public RoleUserTH(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            this.userManager =_userManager;
            this.roleManager = _roleManager;
        }
        //bu tag helperın amacı verilen role id'deki userları bulup ilgili alana yazdırmak.
        [HtmlAttributeName("i-role")]
        public string RoleId { get; set; }//Rolün adını taşıycaz bununla

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> userNames = new List<string>();
            IdentityRole role = await roleManager.FindByIdAsync(RoleId);
            if (role != null)
            {
                foreach (var user in userManager.Users)
                {
                    if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        userNames.Add(user.UserName);
                    }
                }
            }

            //Alttaki yapı ile html contentini değiştirdik
            output.Content.SetContent
                (userNames.Count == 0 ? "No User" : string.Join(" ,", userNames));
            //return base.ProcessAsync(context, output);

        }
    }
}
