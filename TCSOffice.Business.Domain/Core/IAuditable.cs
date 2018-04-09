using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Core
{
    public interface IAuditable
    {
        /// <summary>
        /// This is the date the entity was created
        /// </summary>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// If an entity is deleted but hasn't been purged from the database, this is the date that it was deleted
        /// </summary>
        DateTime? DateDeleted { get; set; }

        /// <summary>
        /// This is the most recent date of the last modification to the record
        /// </summary>
        DateTime? DateLastModified { get; set; }

        DateTime? DateDeactivated { get; set; }
        /// <summary>
        /// This is the date that record was archived
        /// </summary>
        DateTime? DateArchived { get; set; }

        /// <summary>
        /// This is the account who did the last modification
        /// </summary>
        int? LastModifiedBy { get; set; }

        int? CreatedBy { get; set; }

        /// <summary>
        /// This is the account that deleted this item
        /// </summary>
        int? DeletedBy { get; set; }
    }
}
