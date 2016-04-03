using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using bdlSecurity.Models;
using bdlSecurity.ViewModels;
using System.Security.Claims;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Authorization;
using bdlSecurity.Common;
using System.Security.Principal;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace bdlSecurity.Controllers
{
    public class AccountController : Controller
    {
        BadgeUserManager _userManager;
        public AccountController(BadgeUserManager userManager)
        {
            _userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            ViewBag.Message = "User is logged out.";
            await HttpContext.Authentication.SignOutAsync("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string RedirectURL)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.AuthenticateUser(vm.UserName, vm.Password);
                if (user != null)
                {
                    await HttpContext.Authentication.SignInAsync("ApplicationCookie", user.Principal, new AuthenticationProperties()
                    {
                        IsPersistent = false
                    });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user or password");
                    return View();
                }

                if (string.IsNullOrEmpty(RedirectURL))
                { return RedirectToAction("Index", "Home"); }
                else
                { return Redirect(RedirectURL); }
            }
            ModelState.AddModelError("", ValidationHelper.ValidationMessageToString(ModelState));
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
