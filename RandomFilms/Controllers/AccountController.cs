using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<User> signInManager;
        public AccountController(SignInManager<User> _signInManager)
        {
            signInManager = _signInManager;
        }
        [HttpGet]
        public IActionResult Login(string returnurl = null)
        {
            return View(new LoginModel { ReturnUrl = returnurl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Snoopy thinks you're trying to fool him");
                }
            }
            else
            {
                ModelState.AddModelError("", "Snoopy required your password");
            }
            return View(model);
        }
        public IActionResult AccessDenied ()
        {
            return Forbid();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            // удаляем аутентификационные куки
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
       
    }
}
