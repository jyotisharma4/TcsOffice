using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Core;

namespace TCSOffice.Business.Domain.Entity
{
    public class Login : EntityNmDs
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual Company Company { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
