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
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("uspGetAllCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    var d = dt;
                    con.Close();
                }
            }

            return null;
        }
    }
}
