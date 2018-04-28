using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCSOffice.Business.Services;

namespace TCSOffice.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICompanyService _service;

        public HomeController(ICompanyService service)
            : base()
        {
            _service = service;
        }

        // GET: Home
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Account/Login");
        }

       
        public ActionResult loaddata()
        {
            try
            {
                var data = _service.GetAll();
                //Returning Json Data  
                return Json(new { data = data.Data.ToList(),JsonRequestBehavior.AllowGet });

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}