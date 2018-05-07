using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;
using TCSOffice.Business.Domain.Dto;

namespace TCSOffice.Business.Services.Customer
{
    public interface ICustomerService
    {
        BaseResponse<List<CustomerDto>> GetAll(jQueryDataTableParamModel filters);
    }
}
