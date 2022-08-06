using MediumClone.Models;
using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediumClone.Controllers
{   
    public class SignInController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public SignInController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region CreateOperations
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserVM user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser();
                appUser.UserName = user.UserName;
                appUser.Email = user.Email;
                appUser.FirstName = user.FirstName;
                appUser.LastName = user.LastName;                
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                IdentityResult resultRole = await userManager.AddToRoleAsync(appUser, "User");
                if (result.Succeeded && resultRole.Succeeded)
                {                   
                    return RedirectToAction("UserIndex", "Home");//sonradan değiştiriliecek                    
                }
                else
                {
                    Error(result);
                }
            }
            return View(user);
        }

        #endregion
        void Error(IdentityResult result)
        {
            foreach (IdentityError item in result.Errors)
            {
                ModelState.AddModelError("User Operation", $"{item.Code} - {item.Description}");
            }
        }


    }
}
