using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;

namespace TCSOffice.Business.Domain.Entity
{
   public class ZedInfo : EntityNmDs
    {
        public int ZedNumber { get; set; }
        public int Amount { get; set; }
        public virtual DailySalesReports DailySalesReport { get; set; }
    }
}
