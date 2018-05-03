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
using TCSOffice.Business.Domain.Entity;

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

        public BaseResponse<Company> Get(int Id)
        {
            try
            {
                Company company = new Company();
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("getByID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TableName", "Companies");
                        cmd.Parameters.AddWithValue("@Id", Id);
                        con.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        //load into the result object the returned row from the database
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                company.CompanyName = dataReader["CompanyName"].ToString();
                                company.Address = dataReader["Address"].ToString();
                                company.Email = dataReader["Email"].ToString();
                                company.Id = Convert.ToInt32(dataReader["Id"]);
                                company.IsActive = Convert.ToBoolean(dataReader["IsActive"].ToString());
                                company.Phone = dataReader["Phone"].ToString();
                            }
                        }
                        con.Close();
                    }
                }


                return new BaseResponse<Company>
                {
                    Message = "Success",
                    Success = true,
                    Data = company
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Company>
                {
                    Message = ex.Message,
                    Success = false,
                    Data = null
                };
            }
        }

        public BaseResponse<List<CompanyDto>> GetAllCompaniesLstForPreview()
        {
            try
            {
                IEnumerable<CompanyDto> companiesList = new List<CompanyDto>();
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("uspGetAllCompany", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        var dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        companiesList = (from DataRow row in dt.Rows

                                       select new CompanyDto
                                       {
                                           CompanyName = row["CompanyName"].ToString(),
                                           Address = row["Address"].ToString(),
                                           Email = row["Email"].ToString(),
                                           Id = Convert.ToInt32(row["Id"].ToString()),
                                           IsActive = Convert.ToBoolean(row["IsActive"].ToString()),
                                           Phone = row["Phone"].ToString()
                                       });
                        con.Close();
                    }
                }
                return new BaseResponse<List<CompanyDto>>
                {
                    Message = "Success",
                    Success = true,
                    Data = companiesList.ToList()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CompanyDto>>
                {
                    Message = ex.Message,
                    Success = false,
                    Data = null
                };
            }
        }

        public BaseResponse Save(Company dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCompany", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@Name", dto.CompanyName);
                        cmd.Parameters.AddWithValue("@Phone", dto.Phone);
                        cmd.Parameters.AddWithValue("@Email", dto.Email);
                        cmd.Parameters.AddWithValue("@IsActive", dto.IsActive);
                        cmd.Parameters.AddWithValue("@Id", dto.Id);
                        cmd.Parameters.AddWithValue("@Address", dto.Address);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return new BaseResponse
                {
                    Message = "Success",
                    Success = true,
                    Data = null
                };
            }
            catch(Exception ex) {

                return new BaseResponse
                {
                    Message = ex.Message,
                    Success = false,
                    Data = null
                };
            }
        }

    }
}
