using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;

namespace TCSOffice.Business.Domain.Entity
{
  public class CreditSales : EntityNmDs
    {
        public string InvoiceNumber { get; set; }
        public string HT { get; set; }
        public int TVA { get; set; }
        public int Pre { get; set; }
        public int Amount { get; set; }
        public virtual DailySalesReports DailySalesReport { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
