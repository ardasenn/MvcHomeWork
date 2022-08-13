using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MediumClone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public AdminController(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
           
        }
        public IActionResult Index()
        {

            return View(userManager.Users);           
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser admin=await userManager.GetUserAsync(HttpContext.User);
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null && admin.Id!=user.Id)
            {
                await userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddYears(50)));                               
            }
            else
            {
                ModelState.AddModelError("Delete", "You cant delete your account");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }
       
    }
}
