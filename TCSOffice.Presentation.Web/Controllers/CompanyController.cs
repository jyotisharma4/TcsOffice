using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Services;
using TCSOffice.Presentation.Web.CustomAction;

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

        [HttpGet]
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

        //[HttpPost]
        public ActionResult PreviewCompanies()
        {
            try
            {
                var response = _service.GetAllCompaniesLstForPreview();
                if (response.Success)
                {
                    return new ViewAsPdf("CompaniesViewAsPdf", response.Data);
                }
                else
                {
                    TempData["Error"] = response.Message;
                    return View("Index");
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}