using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IAuditable
        {
            return new Repository<TEntity>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
