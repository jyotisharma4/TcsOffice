using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Domain.Entity;

namespace TCSOffice.Business.Services
{
    public interface ICompanyService 
    {
        BaseResponse<List<CompanyDto>> GetAll(jQueryDataTableParamModel filters);
        BaseResponse<Company> Get(int Id);
        BaseResponse<List<CompanyDto>> GetAllCompaniesLstForPreview();
        BaseResponse Save(Company dto);

        // BaseResponse<List<Partial>> GetNotIncludedApplications(IRequester requester, int productId);
        //  BaseResponse<DataResult<ApplicationDto>> GetByProduct(IRequester requester, DataRequest dataRequest, int productId);
    }
}
