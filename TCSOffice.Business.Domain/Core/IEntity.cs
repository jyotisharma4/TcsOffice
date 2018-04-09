using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Core
{
    /// <summary>
    /// This is the interface for most of the Entities in the System
    /// esp those with an Id
    /// </summary>
    public interface IEntity : IId, IAuditable
    {
    }
}
