using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Core
{
    public abstract class Auditable : IAuditable
    {
        /// <summary>
        /// This is the date the entity was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// If an entity is deleted but hasn't been purged from the database, this is the date that it was deleted
        /// </summary>
        public DateTime? DateDeleted { get; set; }

        /// <summary>
        /// This is the most recent date of the last modification to the record
        /// </summary>
        public DateTime? DateLastModified { get; set; }

        public DateTime? DateArchived { get; set; }

        /// <summary>
        /// This is the user who did the last modification
        /// </summary>
        public int? LastModifiedBy { get; set; }
        public DateTime? DateDeactivated { get; set; }
        public int? CreatedBy { get; set; }
        public int? DeletedBy { get; set; }

        protected Auditable()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
