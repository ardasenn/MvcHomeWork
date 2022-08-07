using MediumClone.Models;
using MediumClone.Models.Authentication;
using MediumClone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediumClone.Controllers
{   
    public class SignInController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly SignInManager<AppUser> signInManager;

        public SignInController(UserManager<AppUser> userManager, IEmailSender emailSender, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.signInManager = signInManager;
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
                    // await emailSender.SendEmailAsync(appUser.Email, "Hesap Aktivite işlemi", "Lütfen hesap aktivasyonu yapınız");
                    Microsoft.AspNetCore.Identity.SignInResult resultCheck = await signInManager.PasswordSignInAsync(appUser, user.Password, false, false);
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
