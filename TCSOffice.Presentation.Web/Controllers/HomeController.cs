using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCSOffice.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View("~/Views/Home/Index.cshtml", "~/Views/Shared/_Layout.cshtml");
            }
            return View();
        }
    }
}