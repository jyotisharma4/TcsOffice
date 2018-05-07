using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Services.Customer;
using TCSOffice.Presentation.Web.CustomAction;

namespace TCSOffice.Presentation.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(CustomerService service)
            : base()
        {
            _service = service;
        }
        // GET: Customer
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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerDto dto)
        {
            return View();
        }

    }
}