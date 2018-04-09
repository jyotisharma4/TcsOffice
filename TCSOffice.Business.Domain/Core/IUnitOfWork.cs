using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class, IAuditable;
    }
}
