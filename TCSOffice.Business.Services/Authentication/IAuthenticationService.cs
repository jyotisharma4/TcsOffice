﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;
using TCSOffice.Business.Domain.Dto;

namespace TCSOffice.Business.Services.Authentication
{
    public interface IAuthenticationService
    {
        BaseResponse Login(LoginViewModel login);
        BaseResponse RegisterCompany(LoginViewModel login);
    }
}