using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Dto
{
    public class Response
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
