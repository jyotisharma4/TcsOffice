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

namespace TCSOffice.Business.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        string constring = ConfigurationManager.ConnectionStrings["TCSOfficeContext"].ConnectionString;
        public BaseResponse<List<CustomerDto>> GetAll(jQueryDataTableParamModel filters)
        {
            IEnumerable<CustomerDto> customersList = new List<CustomerDto>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_GetAllCustomers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    customersList = (from DataRow row in dt.Rows

                                   select new CustomerDto
                                   {
                                       CustomerName = row["CustomerName"].ToString(),
                                       ContributionNumber = row["ContributionNumber"].ToString(),
                                       StartCreditPeriod = Convert.ToDateTime(row["StartCreditPeriod"].ToString()),
                                       EndCreditPeriod = Convert.ToDateTime(row["EndCreditPeriod"].ToString()),
                                       CompanyName = row["CompanyName"].ToString(),
                                       Address = row["Address"].ToString(),
                                       Id = Convert.ToInt32(row["Id"].ToString()),
                                       PhoneNumber = Int32.Parse(row["PhoneNumber"].ToString()),
                                       DGEApproved = Convert.ToBoolean(row["DGEApproved"].ToString())
                                   });
                    var d = dt;
                    con.Close();
                }
            }

            IEnumerable<CustomerDto> filteredCustomers;
            #region Searching
            if (!string.IsNullOrEmpty(filters.sSearch))
            {
                filteredCustomers = customersList
                         .Where(c => c.CompanyName.ToLower().Contains(filters.sSearch.ToLower()) ||
                         c.CustomerName.ToLower().Contains(filters.sSearch.ToLower()) ||
                         c.ContributionNumber.ToLower().Contains(filters.sSearch.ToLower()) ||
                         c.StartCreditPeriod.ToString().Contains(filters.sSearch.ToLower()) ||
                         c.EndCreditPeriod.ToString().Contains(filters.sSearch.ToLower()) ||
                         c.Address.ToLower().Contains(filters.sSearch.ToLower()) ||
                         c.DGEApproved.ToString().Contains(filters.sSearch.ToLower()) ||
                         c.PhoneNumber.ToString().Contains(filters.sSearch));
            }
            else
            {
                filteredCustomers = customersList;
            }
            #endregion

            #region Sorting

            Func<CustomerDto, string> orderingFunction = (c => filters.iSortColIndx == 0 ? c.CustomerName :
                                                        filters.iSortColIndx == 1 ? c.ContributionNumber :
                                                        filters.iSortColIndx == 2 ? c.StartCreditPeriod.ToString() :
                                                        filters.iSortColIndx == 3 ? c.EndCreditPeriod.ToString() :
                                                        filters.iSortColIndx == 4 ? c.Address :
                                                        filters.iSortColIndx == 5 ? c.CompanyName :
                                                        filters.iSortColIndx == 6 ? c.PhoneNumber.ToString() :
                                                        c.DGEApproved.ToString()
                                                        );

            if (filters.iSortColDir == "asc")
                filteredCustomers = filteredCustomers.OrderBy(orderingFunction);
            else
                filteredCustomers = filteredCustomers.OrderByDescending(orderingFunction);
            #endregion
            return new BaseResponse<List<CustomerDto>>
            {
                Message = "Success",
                Success = true,
                Data = filteredCustomers.Skip(filters.iDisplayStart).Take(filters.iDisplayLength).ToList()
            };
        }

        public BaseResponse<Response> SaveOrUpdate(CustomerDto customer)
        {
            Response response = null;
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_SaveOrUpdateCustomer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", customer.Id);
                        cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        cmd.Parameters.AddWithValue("@ContributionNumber", customer.ContributionNumber);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);
                        cmd.Parameters.AddWithValue("@City", customer.City);
                        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                        cmd.Parameters.AddWithValue("@MobileNumber", customer.MobileNumber);
                        cmd.Parameters.AddWithValue("@OpeningBalance", customer.OpeningBalance);
                        cmd.Parameters.AddWithValue("@CreditLimit", customer.CreditLimit);
                        cmd.Parameters.AddWithValue("@StartCreditPeriod", customer.StartCreditPeriod);
                        cmd.Parameters.AddWithValue("@EndCreditPeriod", customer.EndCreditPeriod);
                        cmd.Parameters.AddWithValue("@DGEApproved", customer.DGEApproved);
                        cmd.Parameters.AddWithValue("@FaxNumber", customer.FaxNumber);
                        cmd.Parameters.AddWithValue("@Attendent", customer.Attendent);
                        cmd.Parameters.AddWithValue("@CompanyId", customer.CompanyId);
                        cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                        SqlParameter parameter = new SqlParameter();
                        parameter = new SqlParameter("@message", SqlDbType.VarChar, 255);
                        parameter.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(parameter);
                        parameter = new SqlParameter("@customerId", DbType.Int64);
                        parameter.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(parameter);
                        parameter = new SqlParameter("@statusCode", DbType.Int64);
                        parameter.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(parameter);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        response = new Response();
                        response.Message = cmd.Parameters["@message"].Value.ToString();
                        response.Id = Int32.Parse(cmd.Parameters["@customerId"].Value.ToString());
                        response.StatusCode = Int32.Parse(cmd.Parameters["@statusCode"].Value.ToString());
                        con.Close();

                        if(response != null)
                        {
                            return ResponseFactory.SuccessForType<Response>(response, "Success.");
                        }
                    }
                }
                return ResponseFactory.ErrorForType<Response>("Something went wrong.");
            }
            catch (Exception ex)
            {
                return ResponseFactory.ErrorForType<Response>(ex.Message);
            }
        }
    }
}
