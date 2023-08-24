using KUSYS.Core.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace KUSYS.DataAccess.Repositories.Base
{
    public class Repository<TEntity, TDbContext> : IRepository<TEntity, TDbContext>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; }
        private readonly DbSet<TEntity> _table;

        public Repository(TDbContext context)
        {
            Context = context;
            _table = Context.Set<TEntity>();
        }

        public IQueryable<TEntity> AsQueryable()
            => _table.AsQueryable();

        public IIncludableQueryable<TEntity, TProp> Include<TProp>(Expression<Func<TEntity, TProp>> navigationPropertyPath)
            => _table.Include(navigationPropertyPath);

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
            => _table.Where(expression);

        public IQueryable<TEntity> WhereNoTracking(Expression<Func<TEntity, bool>> expression)
            => _table.AsNoTracking().Where(expression);

        public TEntity FirstOrDefaultNoTracking(Expression<Func<TEntity, bool>> expression)
            => _table.AsNoTracking().Where(expression).FirstOrDefault();

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
            => _table.Where(expression).FirstOrDefault();

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
            => await _table.Where(expression).FirstOrDefaultAsync(cancellationToken);

        public async Task<TEntity> FirstOrDefaultNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default!)
            => await _table.AsNoTracking().Where(expression).FirstOrDefaultAsync(cancellationToken);

        public bool Any(Expression<Func<TEntity, bool>> expression)
            => _table.Any(expression);

        public TEntity Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(TEntity entity)
            => Context.Entry(entity).State = EntityState.Deleted;

        public bool SaveChanges()
            => Context.SaveChanges() > 0;

        public async Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities)
        {
            Context.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Context.AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var entity in entities.AsParallel().WithCancellation(cancellationToken))
                    Context.Entry(entity).State = EntityState.Modified;
            }
            catch (OperationCanceledException)
            {
                foreach (var entity in entities)
                    Context.Entry(entity).State = EntityState.Unchanged;
            }
            return entities;
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var entity in entities.AsParallel().WithCancellation(cancellationToken))
                    Context.Entry(entity).State = EntityState.Deleted;
            }
            catch (OperationCanceledException)
            {
                foreach (var entity in entities)
                    Context.Entry(entity).State = EntityState.Unchanged;
            }
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
            => (await Context.SaveChangesAsync(cancellationToken)) > 0;
    }
}
