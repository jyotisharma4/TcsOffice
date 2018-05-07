using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Contribution Number")]
        public string ContributionNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Mobile Number")]
        public int MobileNumber { get; set; }

        [Display(Name = "Opening Balance")]
        public int OpeningBalance { get; set; }

        [Display(Name = "Credit Limit")]
        public int CreditLimit { get; set; }

        [Display(Name = "Start Credit Period")]
        public DateTime? StartCreditPeriod { get; set; }

        [Display(Name = "End Credit Period")]
        public DateTime? EndCreditPeriod { get; set; }

        [Display(Name = "DGE Approved")]
        public bool DGEApproved { get; set; }

        [Display(Name = "Fax Number")]
        public int FaxNumber { get; set; }

        public string Attendent { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public int CompanyId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }
    }
}
