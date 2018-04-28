using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;
using TCSOffice.Business.Domain.Dto;

namespace TCSOffice.Business.Services
{
   public class CompanyService : ICompanyService
    {
      string constring = ConfigurationManager.ConnectionStrings["TCSOfficeContext"].ConnectionString;
        public BaseResponse<List<CompanyDto>> GetAll(jQueryDataTableParamModel filters)
        {
            IEnumerable<CompanyDto> companyList = new List<CompanyDto>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("uspGetAllCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                   companyList =(from DataRow row in dt.Rows

                     select new CompanyDto
                     {
                         CompanyName = row["CompanyName"].ToString(),
                         Address = row["Address"].ToString(),
                         Email = row["Email"].ToString(),
                         Id = Convert.ToInt32(row["Id"].ToString()),
                         IsActive =Convert.ToBoolean(row["IsActive"].ToString()),
                         Phone = row["Phone"].ToString()
                     });
                    var d = dt;
                    con.Close();
                }
            }

            IEnumerable<CompanyDto> filteredCompanies;
            #region Searching
            if (!string.IsNullOrEmpty(filters.sSearch))
            {
                filteredCompanies = companyList
                         .Where(c => c.CompanyName.ToLower().Contains(filters.sSearch.ToLower())||
                             c.Address.ToLower().Contains(filters.sSearch.ToLower()) ||
                             c.Email.ToLower().Contains(filters.sSearch.ToLower()) ||
                             c.Phone.Contains(filters.sSearch));
            }
            else
            {
                filteredCompanies = companyList;
            }
            #endregion

            #region Sorting

            Func<CompanyDto, string> orderingFunction = (c => filters.iSortColIndx == 0 ? c.CompanyName :
                                                        filters.iSortColIndx == 1 ? c.Email : 
                                                        filters.iSortColIndx == 2 ? c.Address :
                                                        c.Phone
                                                        );

            if (filters.iSortColDir == "asc")
                filteredCompanies = filteredCompanies.OrderBy(orderingFunction);
            else
                filteredCompanies = filteredCompanies.OrderByDescending(orderingFunction);
            #endregion
            return new BaseResponse<List<CompanyDto>>
            {
                Message = "Success",
                Success = true,
                Data = filteredCompanies.Skip(filters.iDisplayStart).Take(filters.iDisplayLength).ToList()
            };
        }
    }
}
