using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Core
{
    /// <summary>
    /// Abstract base object of the system.
    /// You can inherit directly from this one, or if you need to add some custom properties to your base object then inherit from IEntity
    /// </summary>
    public abstract class EntityNmDs : Auditable, IEntity
    {
        /// <summary>
        /// This is the Id of the object in the database
        /// </summary>
        public virtual int Id { get; set; }
    }
}
