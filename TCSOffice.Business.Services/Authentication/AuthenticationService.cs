using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.DataAccess;
using TCSOffice.Business.Domain.Core;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Domain.Entity;
using TCSOffice.Business.Domain.Utilities.Helpers;
using TCSOffice.Business.Services.Authentication.Workers;

namespace TCSOffice.Business.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
        string fromPassword = ConfigurationManager.AppSettings["fromPassword"];
        string basePath = ConfigurationManager.AppSettings["basePath"];

        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        TCSOfficeDbContext TCSOfficeDbContext = new TCSOfficeDbContext();

        public BaseResponse Login(LoginViewModel login)
        {
            var isExists = TCSOfficeDbContext.Logins.Where(z => z.UserName == login.UserName && z.Password == login.Password && z.IsActive == true).ToList();
            if (isExists.Count > 0)
            {
                return ResponseFactory.Success(isExists);
            }
            return ResponseFactory.Success();
        }

        public BaseResponse RegisterCompany(LoginViewModel login)
        {
            try
            {
                var companyEntity = login.ToEntity();
                TCSOfficeDbContext.Companies.Add(companyEntity);
                TCSOfficeDbContext.SaveChanges();
                if (companyEntity != null)
                {
                    var userEntity = new Login
                    {
                        UserName = login.UserName,
                        Password = login.Password,
                        Email = login.Email,
                        IsActive = false,
                        IsAdmin = false,
                        Company = companyEntity
                    };
                    TCSOfficeDbContext.Logins.Add(userEntity);
                    TCSOfficeDbContext.SaveChanges();

                    // Send email to admin
                    var getAdminEntity = TCSOfficeDbContext.Logins.FirstOrDefault(z => z.IsAdmin == true);
                    if (getAdminEntity != null)
                    {
                        string subject = "Activate Company for " + login.UserName;
                        var body = new StringBuilder();
                        body.AppendFormat("Hello Admin,\n");
                        body.AppendLine("Please click on below link to activate the company.");
                        body.AppendLine("<a href=" + basePath + "Account/ActivateCompanyFromEmail?userId=" + userEntity.Id + "&companyId=" + companyEntity.Id + ">Activate</a>");

                        string emailResult = Mail.SendMail(getAdminEntity.Email, body.ToString(), subject, fromEmail, fromPassword);
                        if (emailResult == "OK")
                        {
                            return ResponseFactory.Success(null, "Congratulations! Your account has successfully created. Please ask your administrator to activate your account or wait for next 24 working hours.");
                        }
                    }
                }
                return ResponseFactory.Error("Something went wrong.");

            }
            catch (Exception ex)
            {
                //if (ex.GetBaseException().GetType() == typeof(SqlException))
                //{
                //    Int32 ErrorCode = ((SqlException)ex.InnerException).Number;
                //    string errorMessage = string.Empty;
                //    switch (ErrorCode)
                //    {
                //        case 4060: // Invalid Database
                //            errorMessage = "Invalid Database";
                //            break;
                //        case 18456: // Login Failed
                //            errorMessage = "Login failed";
                //            break;
                //        case 547: // ForeignKey Violation
                //            errorMessage = "ForeignKey Violation";
                //            break;
                //        case 2627: // Unique Index/ Primary key Violation/ Constriant Violation
                //            errorMessage = "Unique key violation in email, company name or username";
                //            break;
                //        case 2601: // Unique Index/Constriant Violation
                //            errorMessage = "Unique key violation in email, company name or username";
                //            break;
                //        default:
                //            errorMessage = "Something went wrong";
                //            break;
                //    }
                //    return ResponseFactory.Error(errorMessage);
                //}
                return ResponseFactory.Error(ex.Message);
            }
        }

        public BaseResponse<LoginViewModel> GetCompany(int userId, int companyId)
        {
            LoginViewModel viewModel = null;
            if (userId > 0 && companyId > 0)
            {
                var getUser = TCSOfficeDbContext.Logins.FirstOrDefault(z => z.Id == userId);
                if (getUser != null)
                {
                    var getCompany = TCSOfficeDbContext.Companies.FirstOrDefault(z => z.Id == companyId);
                    if (getCompany != null)
                    {
                        viewModel = new LoginViewModel();
                        viewModel.CompanyName = getCompany.CompanyName;
                        viewModel.Address = getCompany.Address;
                        viewModel.Phone = getCompany.Phone;
                        viewModel.Email = getUser.Email;
                        viewModel.UserName = getUser.UserName;
                        return ResponseFactory.SuccessForType<LoginViewModel>(viewModel, "Successfully fetched details.");
                    }
                }
                return ResponseFactory.ErrorForType<LoginViewModel>("Something went wrong.");
            }
            return ResponseFactory.ErrorForType<LoginViewModel>("Company is invalid.");
        }

        public BaseResponse ActivateCompany(string companyId, string userId)
        {
            try
            {
                int parseUserId = Int32.Parse(userId);
                var userEntity = TCSOfficeDbContext.Logins.FirstOrDefault(z => z.Id == parseUserId);
                if (userEntity != null)
                {
                    userEntity.IsActive = true;
                    userEntity.DateLastModified = DateTime.UtcNow;
                    TCSOfficeDbContext.SaveChanges();
                    return ResponseFactory.Success(null, "Activated successfully.");
                }
                return ResponseFactory.Error("User doesn't exist.");
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public BaseResponse SendPassword(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var userEntity = TCSOfficeDbContext.Logins.FirstOrDefault(z => z.Email == email);
                    if (userEntity != null)
                    {
                        // Send email
                        string subject = "Reset Password for TCS Office";
                        var body = new StringBuilder();
                        body.AppendFormat("Hello " + userEntity.UserName);
                        body.AppendLine("Your password - ");
                        body.AppendLine(userEntity.Password);

                        string emailResult = Mail.SendMail(email, body.ToString(), subject, fromEmail, fromPassword);
                        if (emailResult == "OK")
                        {
                            return ResponseFactory.Success(null, "Password has been sent to your registered email id.");
                        }
                    }
                    else
                    {
                        return ResponseFactory.Error("Please enter valid email id.");
                    }
                }
                return ResponseFactory.Error("Something went wrong.");
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
