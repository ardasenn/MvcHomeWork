using MediumClone.Models;
using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediumClone.Controllers
{
    public class LogInController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public LogInController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            { AppUser appUser = await userManager.FindByEmailAsync(login.Email);
              if(appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =  await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);                  
                    if (result.Succeeded) return RedirectToAction("UserIndex", "Home",appUser);                    
              }
                ModelState.AddModelError("User Operations", $"Login Failed with{login.Email}. Invalid Email or Password");
            }            
            return View(login);            
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
