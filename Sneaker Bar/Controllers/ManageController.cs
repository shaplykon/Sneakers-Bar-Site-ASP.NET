using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Sneaker_Bar.Controllers
{
    public class ManageController : Controller
    {
        private UserManager<IdentityUser> userManager;
        public ManageController(UserManager<IdentityUser> _userManager) {
            userManager = _userManager;
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin, manager")]
        [HttpGet]
        public IActionResult Index()
        {
            List<IdentityUser> users = userManager.Users.ToList<IdentityUser>();
            ViewBag.users = users;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAsync(string action, string userId, string role) {
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);
            if (action.Equals("delete"))
            {
                var remFromRole = await userManager.RemoveFromRoleAsync(user, role);
                await userManager.DeleteAsync(user);
            }
            if (action.Equals("edit"))
            {

                var remFromRole = await userManager.RemoveFromRoleAsync(user, role);
                if (role.Equals("user"))
                {
                    await userManager.AddToRoleAsync(user, "manager");
                }
                if (role.Equals("manager")) {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
