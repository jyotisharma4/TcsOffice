using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;

namespace TCSOffice.Business.Domain.Entity
{
   public class DailySalesReports : EntityNmDs
    {
        [Required]
        public int DSRNumber { get; set; }
        public DateTime DSRDate { get; set; }
        public int TotalSale { get; set; }
        public int TotalReceiptAmount { get; set; }
        public int CashReceived { get; set; }
        public int TotalChequesReceived { get; set; }
        public int TotalExpenses { get; set; }
        public int TotalCreditSale { get; set; }
        public string Remarks { get; set; }
    }
}
