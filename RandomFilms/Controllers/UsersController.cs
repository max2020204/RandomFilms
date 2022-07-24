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
    public class UsersController : Controller
    {
        UserManager<User> userManager;
        public UsersController(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(RegisterModel model)
        {
            if (ModelState.IsValid && userManager.FindByNameAsync(model.Login).Result?.UserName == null && userManager.FindByEmailAsync(model.Email).Result?.Email == null)
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Login,
                    Email = model.Email
                };
                // добавляем пользователя

                var result = await userManager.CreateAsync(user, model.Password);
                var roleres = userManager.AddToRoleAsync(user, "User");
                if (result.Succeeded && roleres.Result.Succeeded)
                {
                    model.State = 1;
                    return View(model);
                }
                else
                {
                    model.State = 2;
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                model.State = 2;
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", ModelState.ValidationState.ToString());
                }
                if (userManager.FindByEmailAsync(model.Email).Result?.Email != null)
                {
                    ModelState.AddModelError("", "This email is already in use.");
                }
                if (userManager.FindByNameAsync(model.Login).Result?.UserName != null)
                {
                    ModelState.AddModelError("", "This login is already in use.");
                }


            }
            return View(model);
        }
        public IActionResult AllUsers()
        {
            return View(userManager.Users);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("AllUsers");
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordModel model = new ChangePasswordModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                    HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                    await _passwordValidator.ValidateAsync(userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
    }
}
