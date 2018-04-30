using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
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
                if (response.Message == "Admin")
                {
                    return RedirectToAction("Index", "Company");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ActivateCompanyFromEmail(int userId, int companyId)
        {
            var response = _authenticationService.GetCompany(userId, companyId);
            if(response.Success)
            {
                TempData["CompanyId"] = companyId;
                TempData["UserId"] = userId;
                return View(response.Data);
            }
            TempData["Error"] = response.Message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateCompanyFromEmail()
        {
            string companyId = TempData.Peek("CompanyId").ToString();
            string userId = TempData.Peek("UserId").ToString();
            var response = _authenticationService.ActivateCompany(companyId, userId);
            if (response.Success)
            {
                TempData["Success"] = response.Message;
            }
            else
            {
                TempData["Error"] = response.Message;
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SendPasswordByEmail(LoginViewModel login)
        {
            var response = _authenticationService.SendPassword(login.Email);
            if (response.Success)
            {
                TempData["Success"] = response.Message;
            }
            else
            {
                TempData["Error"] = response.Message;
            }
            return RedirectToAction("Login", "Account");
        }
    }
}