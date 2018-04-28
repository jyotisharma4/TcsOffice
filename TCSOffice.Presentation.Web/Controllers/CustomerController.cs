using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCSOffice.Presentation.Web.CustomAction;

namespace TCSOffice.Presentation.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrintInvoice()
        {
            return new ViewAsPdf("CustomerViewAsPdf");
        }
    }
}