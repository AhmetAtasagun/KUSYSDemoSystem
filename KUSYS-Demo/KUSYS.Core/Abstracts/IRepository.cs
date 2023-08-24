using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace KUSYS.Core.Abstracts
{
    public interface IRepository<TEntity, TDbContext>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        #region Queries
        IQueryable<TEntity> AsQueryable();
        IIncludableQueryable<TEntity, TProp> Include<TProp>(Expression<Func<TEntity, TProp>> navigationPropertyPath);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Tracking takibini devredışı bırakarak sorgulama yapar.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<TEntity> WhereNoTracking(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Tracking takibini devredışı bırakarak Tek kayıt veya değersiz veri getirir.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        TEntity FirstOrDefaultNoTracking(Expression<Func<TEntity, bool>> expression);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FirstOrDefaultNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default!);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default!);
        bool Any(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region Commands
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        bool SaveChanges();
        Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default!);
        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default!);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default!);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default!);
        #endregion
    }
}
