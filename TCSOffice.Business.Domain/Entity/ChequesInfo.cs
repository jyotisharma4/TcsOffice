using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;

namespace TCSOffice.Business.Domain.Entity
{
  public class ChequesInfo : EntityNmDs
    {
        public string ChequeNumber { get; set; }
        public DateTime IssuedFrom { get; set; }
        public int Amount { get; set; }
        public string Remarks { get; set; }
        public virtual DailySalesReports DailySalesReport { get; set; }
    }
}
