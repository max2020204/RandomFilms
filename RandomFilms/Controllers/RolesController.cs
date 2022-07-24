using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Controllers
{
    [Authorize(Roles = "Moderator,Admin")]
    [Route("Admin/[controller]/[action]")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<User> userManager;
        public RolesController(RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View(roleManager.Roles);
        }
        public IActionResult Create(int id)
        {
            CreateRoleModel role = new CreateRoleModel
            {
                Id = id,
            };
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Create", new { id = 1 });
                }
                else
                {
                    if (result.Errors.Select(x => x.Code == "DuplicateRoleName").Count() > 0)
                    {
                       return RedirectToAction("Create", new { id = 3 });
                    }
                    //foreach (var error in result.Errors)
                    //{
                    //    ModelState.AddModelError(string.Empty, error.Description);
                    //}
                }
            }

            return RedirectToAction("Create", new { id = 2 });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                ChangeRoleModel model = new ChangeRoleModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles,
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);

                await userManager.RemoveFromRolesAsync(user, removedRoles);
                ChangeRoleModel model = new ChangeRoleModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles,
                    State = 1
                };
                return View(model);
            }

            return NotFound();
        }
    }
}
