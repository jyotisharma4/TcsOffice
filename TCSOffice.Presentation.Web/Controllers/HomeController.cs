using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCSOffice.Business.Domain.Dto;
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
            var companyId = Session["SelectedCompanyId"];
            return View();
        }
    }
}