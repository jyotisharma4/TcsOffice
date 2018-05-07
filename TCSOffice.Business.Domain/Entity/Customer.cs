using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;

namespace TCSOffice.Business.Domain.Entity
{
    public class Customer : EntityNmDs
    {
        public string CustomerName { get; set; }
        public string ContributionNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PhoneNumber { get; set; }
        public int MobileNumber { get; set; }
        public int OpeningBalance { get; set; }
        public int CreditLimit { get; set; }
        public DateTime? StartCreditPeriod { get; set; }
        public DateTime? EndCreditPeriod { get; set; }
        public bool? DGEApproved { get; set; }
        public int FaxNumber { get; set; }
        public string Attendent { get; set; }
        public virtual Company Company { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
