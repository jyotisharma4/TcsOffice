﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;
using TCSOffice.Business.Domain.Dto;

namespace TCSOffice.Business.Services
{
    public interface ICompanyService 
    {
        BaseResponse<List<CompanyDto>> GetAll();

       // BaseResponse<List<Partial>> GetNotIncludedApplications(IRequester requester, int productId);
      //  BaseResponse<DataResult<ApplicationDto>> GetByProduct(IRequester requester, DataRequest dataRequest, int productId);
    }
}