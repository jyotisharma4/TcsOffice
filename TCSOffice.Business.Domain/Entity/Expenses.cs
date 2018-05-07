using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;

namespace TCSOffice.Business.Domain.Entity
{
   public class Expenses : EntityNmDs
    {
        public string Heading { get; set; }
        public int Amount { get; set; }
        public int Remarks { get; set; }
        public virtual DailySalesReports DailySalesReport { get; set; }
    }
}
