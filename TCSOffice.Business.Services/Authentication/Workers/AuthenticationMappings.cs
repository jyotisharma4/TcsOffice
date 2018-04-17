using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Dto;
using TCSOffice.Business.Domain.Entity;
using TCSOffice.Business.Domain.Utilities.Extensions;

namespace TCSOffice.Business.Services.Authentication.Workers
{
    public static class AuthenticationMappings
    {
        public static Company ToEntity(this LoginViewModel dto, Company entity = null)
        {
            entity = entity == null
                ? dto.MapTo<Company>()
                : dto.MapTo(entity);

            return entity;
        }
    }
}
