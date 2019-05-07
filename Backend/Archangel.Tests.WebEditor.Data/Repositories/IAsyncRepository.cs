using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Archangel.Tests.WebEditor.Data.Repositories
{
    public interface IAsyncRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> GetQuery();
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> criteria);
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> criteria);
        Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> criteria);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria);
    }
}