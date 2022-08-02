using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediumClone.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public AdminController(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
            // bu sınıfı admine has yapmak için nasıl bir yol izlemeliyiz.
        }
        public IActionResult Index()
        {
            return View();           
        }
       
    }
}
