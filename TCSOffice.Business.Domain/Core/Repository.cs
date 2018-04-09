using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Core
{
    public class Repository<T> : IRepository<T> where T : class, IAuditable
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the first item that meets the criteria
        /// Override the GetAllExtended method to ensure the extended entities are attached
        /// eg: Account.Emails, Account.Telephones etc
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return GetAllExtended().FirstOrDefault(predicate);
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        /// <summary>
        /// Get all items from the repository
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return GetAllNotDeletedEntities();
        }

        /// <summary>
        /// This will be overriden by the child classes as they get to specify 
        /// the actual structure of the extended entities
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAllExtended()
        {
            return GetAll();
        }

        public virtual IQueryable<T> GetAllDeleted()
        {
            return GetAllDeletedEntities();
        }

        /// <summary>
        /// Add an item to the repository
        /// </summary>
        /// <param name="item"></param>
        /// <param name="commit">Save changes immediately?</param>
        /// <returns></returns>
        public virtual T Add(T item, bool commit = true)
        {
            _context.Set<T>().Add(item);
            if (commit) CommitChanges();
            return item;
        }

        /// <summary>
        /// Add a batch of items to repo
        /// </summary>
        /// <param name="items"></param>
        /// <param name="commit">Save changes immediately?</param>
        public virtual void Add(IEnumerable<T> items, bool commit = true)
        {
            foreach (var item in items)
            {
                _context.Set<T>().Add(item);
            }
            if (commit) CommitChanges();
        }

        /// <summary>
        /// Update an item in the repository
        /// </summary>
        /// <param name="item"></param>
        /// <param name="commit">Save changes immediately?</param>
        /// <returns></returns>
        public virtual T Update(T item, bool commit = true)
        {
            item.DateLastModified = SystemData.Now();
            _context.Entry(item).State = EntityState.Modified;
            if (commit) CommitChanges();

            return item;
        }

        /// <summary>
        /// This marks item as deleted... it does NOT delete items permanently from the DB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="deletedById">contact id of the person doing the delete</param>
        /// <param name="commit"></param>
        public virtual void DeleteAll(Expression<Func<T, bool>> predicate, int? deletedById = null, bool commit = true)
        {
            var items = GetAll(predicate);
            foreach (var item in items)
            {
                item.DateDeleted = SystemData.Now();
                item.DeletedBy = deletedById;
            }
            if (commit) CommitChanges();
        }

        /// <summary>
        /// Delete all items that match a certain condition from the DB COMPLETELY!
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="commit"></param>
        public virtual void DeleteAllPermanently(Expression<Func<T, bool>> predicate, bool commit = true)
        {
            var items = GetAll(predicate);
            foreach (var item in items)
            {
                _context.Set<T>().Remove(item);
            }
            if (commit) CommitChanges();
        }

        private void CommitChanges()
        {
            _context.SaveChanges();
        }

        private IQueryable<T> GetAllDeletedEntities()
        {
            return _context.Set<T>().Where(x => x.DateDeleted != null);
        }

        private IQueryable<T> GetAllNotDeletedEntities()
        {
            return _context.Set<T>().Where(x => x.DateDeleted == null);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
