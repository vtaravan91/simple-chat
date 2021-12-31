using SimpleChat.DataAccess.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleChat.DataAccess.Repository.Base
{
    public interface IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
        Task<TKey> InsertAndGetIdAsync(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
    }
}
