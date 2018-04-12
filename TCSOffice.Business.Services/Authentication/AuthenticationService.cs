using System;
using System.Collections.Generic;
using System.Configuration;
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
                    string subject = "Activate Company for " + login.UserName;
                    var body = new StringBuilder();
                    body.AppendFormat("Hello Admin,\n");
                    body.AppendLine("Please click on below link to activate the company.");
                    body.AppendLine("<a href=" + basePath + "?userId=" + userEntity.Id + "&companyId=" + companyEntity.Id + ">Activate</a>");

                    string emailResult = Mail.SendMail(login.Email, body.ToString(), subject, fromEmail, fromPassword);
                    if (emailResult == "OK")
                    {
                        return ResponseFactory.Success(null, "Congratulations! Your account has successfully created. Please ask your administrator to activate your account or wait for next 24 working hours.");
                    }
                }
                return ResponseFactory.Error("Something went wrong.");

            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public BaseResponse<LoginViewModel> ActivateCompany(int userId, int companyId)
        {
            if(userId > 0 && companyId > 0)
            {

                var getUser = TCSOfficeDbContext.Logins.FirstOrDefault(z => z.Id == userId);
                if(getUser != null)
                {
                    var getCompany = TCSOfficeDbContext.Companies.FirstOrDefault(z => z.Id == companyId);
                }
            }
            return ResponseFactory.ErrorForType<LoginViewModel>("Company is invalid.");
        }
    }
}
