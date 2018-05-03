using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Domain.Entity;
using TCSOffice.Business.Services;
using TCSOffice.Presentation.Web.CustomAction;
using SpreadsheetLight;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using TCSOffice.Presentation.Web.CustomAction.Excel;

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
           var Data = _service.Get(id);
            return View(Data.Data);
        }

        public ActionResult Detail(int id)
        {
            var Data = _service.Get(id);
            return View(Data.Data);
        }


        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                var Data = _service.Save(company);
                return RedirectToAction("Index");
            }
            return View();
        }

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

        public ActionResult ExportCompaniesToExcel()
        {
            try
            {
                var response = _service.GetAllCompaniesLstForPreview();
                if (response.Success)
                {
                    List<string> columns = new List<string>();
                    foreach (var prop in typeof(CompanyDto).GetProperties())
                    {
                        columns.Add(prop.Name.ToString());
                    }
                    byte[] filecontent = ExcelExportHelpers.ExportExcel(response.Data, "Company", true, columns.ToArray());
                    return File(filecontent, ExcelExportHelpers.ExcelContentType, "CompaniesList_"+Guid.NewGuid()+".xlsx");
                    //return View("Index");
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