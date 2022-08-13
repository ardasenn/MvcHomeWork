using MediumClone.Models;
using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MediumClone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole> _roleManager,UserManager<AppUser> _userManager)
        {
            this.roleManager = _roleManager;
            this.userManager = _userManager;
        }
        
        public ActionResult Index()
        {
            return View(roleManager.Roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Required] string roleName)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result =
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else   Error(result);
            }
            return View();
        }
        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();

            foreach (AppUser user in userManager.Users)
            {
                var list = await
                    userManager.IsInRoleAsync(user,role.Name) ? members : nonMembers; // bu kod bloğu ile kullanıcının hangi lsite ekleneceğini bulup list değerini members yada nonmembers yapıyor dinamik bir kod bloğu bu.
                list.Add(user);
            }

            return View(new RoleEditVM
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoleModificationVM model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Error(result);
                    }
                }
                foreach (string userId in model.DeletedIds ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result =
                            await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Error(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Error(result);
            }
            return View("Index", roleManager.Roles);
        }
        private void Error(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("Role Error", $"{item.Code} - {item.Description}");
            }
        }
    }
}
