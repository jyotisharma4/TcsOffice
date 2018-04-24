using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TCSOffice.Business.Services.Authentication;

namespace TCSOffice.Presentation.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public BaseApiController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
    }
}