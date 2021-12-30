using SimpleChat.DataAccess.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleChat.DataAccess.Repository.Base
{
    public interface IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
        TKey InsertAndGetId(TEntity entity);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
        TEntity GetById(TKey id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
    }
}
