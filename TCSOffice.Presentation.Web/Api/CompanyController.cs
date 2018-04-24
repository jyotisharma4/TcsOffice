using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TCSOffice.Business.Domain.Core;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Services;
using TCSOffice.Business.Services.Authentication;
using TCSOffice.Presentation.Web.Controllers;

namespace TCSOffice.Presentation.Web.Api
{
    public class CompanyController : BaseApiController
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service, IAuthenticationService authenticationService)
            : base(authenticationService)
        {
            _service = service;
        }

       /// [ActionName("Default")]
        [HttpGet]
        public BaseResponse<List<CompanyDto>> Get()
        {
            var data = _service.GetAll();
            return data;
        }
    }
}
