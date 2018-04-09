using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.DataAccess;
using TCSOffice.Business.Domain.Core;
using TCSOffice.Business.Domain.Dto;

namespace TCSOffice.Business.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        TCSOfficeDbContext TCSOfficeDbContext = new TCSOfficeDbContext();
        
        public BaseResponse Login(LoginViewModel login)
        {
            var isExists = TCSOfficeDbContext.Logins.Where(z => z.UserName == login.UserName && z.Password == login.Password).ToList();
            if (isExists.Count > 0)
            {
                return ResponseFactory.Success(isExists);
            }
            return ResponseFactory.Success();
        }
    }
}
