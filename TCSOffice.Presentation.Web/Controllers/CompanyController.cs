using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Services;

namespace TCSOffice.Presentation.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;

        public CompanyController(CompanyService service)
            : base()
        {
            _service = service;
        }
        
        // GET: Company
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Edit(int id)
        {
            return View();
        }

    }
}