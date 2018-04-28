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
            if (Request.IsAuthenticated)
            {

                // var data = _service.GetAll();
                return View();
            }
            return RedirectToAction("Account/Login");
        }


        public ActionResult loaddata(jQueryDataTableParamModel param)
        {
            try
            {
                param.iSortColIndx = Convert.ToInt32(Request["iSortCol_0"]);
                param.iSortColDir = Request["sSortDir_0"];
                var data = _service.GetAll(param);
                //Returning Json Data  
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = data.Data.Count(),
                    iTotalDisplayRecords = data.Data.Count(),
                    aaData = data.Data
                }, JsonRequestBehavior.AllowGet
                );
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}