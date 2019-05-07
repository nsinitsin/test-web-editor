using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Archangel.Tests.WebEditor.Data.Repositories
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : class
    {
#if DEBUG
        private Guid debugId = Guid.NewGuid();
#endif

        private readonly WebEditorDbContext dbContext;
        private bool disposed = false;

        public AsyncRepository(WebEditorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        

        public IQueryable<TEntity> GetQuery()
        {
            return dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Where(criteria);
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().SingleAsync(criteria);
        }

        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().FirstAsync(criteria);
        }

        public TEntity Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return dbContext.Set<TEntity>().Add(entity).Entity;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> criteria)
        {
            var entities = dbContext.Set<TEntity>().Where(criteria);

            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
        
        public Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return Task.FromResult(GetQuery().Where(criteria));
        }

        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Where(criteria).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(dbContext.Set<TEntity>().AsEnumerable());
        }
        
        public Task<int> CountAsync()
        {
            return GetQuery().CountAsync();
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().CountAsync(criteria);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (dbContext != null)
                        dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}