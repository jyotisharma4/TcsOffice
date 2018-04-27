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
        public BaseResponse<List<CompanyDto>> GetAll()
        {
            var companyList = new List<CompanyDto>();
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
                     }).ToList();
                    var d = dt;
                    con.Close();
                }
            }
            return new BaseResponse<List<CompanyDto>>
            {
                Message = "Success",
                Success = true,
                Data = companyList
            };
        }
    }
}
