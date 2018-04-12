using System;
using System.Collections.Generic;
using System.Globalization;
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
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password.");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCompany(LoginViewModel login)
        {
            var response = _authenticationService.RegisterCompany(login);
            if (response.Success)
            {
                TempData["Success"] = response.Message;
            }
            else
            {
                TempData["Error"] = response.Message;
            }
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateCompanyFromEmail(string userId, string companyId)
        {
            return View();
        }
    }
}