using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Core
{
    public interface IRepository<T> : IDisposable
                where T : class, IAuditable
    {
        /// <summary>
        /// Get the first item that meets the criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get all items from the repository
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Override this method to get an entity with all its attachments
        /// This getallextended is used when GetByPredicate is used
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllExtended();

        /// <summary>
        /// Get all items from the repository that have been deleted
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllDeleted();

        /// <summary>
        /// Add an item to the repository
        /// </summary>I
        /// <param name="item"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        T Add(T item, bool commit = true);

        /// <summary>
        /// Update an item in the repository
        /// </summary>
        /// <param name="item"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        T Update(T item, bool commit = true);

        /// <summary>
        /// Delete all items that match a certain condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="deletedById"></param>
        /// <param name="commit"></param>
        void DeleteAll(Expression<Func<T, bool>> predicate, int? deletedById = null, bool commit = true);

        /// <summary>
        /// This deletes the items PERMANENTLY from the DB. This cannot be reversed
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="commit"></param>
        void DeleteAllPermanently(Expression<Func<T, bool>> predicate, bool commit = true);
    }
}
