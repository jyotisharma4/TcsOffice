using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Services.Authentication;

namespace TCSOffice.Presentation.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            var response = _authenticationService.Login(login);
            if (response.Data != null)
            {
                FormsAuthentication.SetAuthCookie(login.UserName, false);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password.");
            }
            return View();
        }
    }
}