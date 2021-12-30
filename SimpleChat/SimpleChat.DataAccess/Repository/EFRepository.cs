using Microsoft.EntityFrameworkCore;
using SimpleChat.DataAccess.DBContext;
using SimpleChat.DataAccess.Entities.Base;
using SimpleChat.DataAccess.Repository.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleChat.DataAccess.Repository
{
    internal sealed class EFRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {

        private readonly SimpleChatContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EFRepository(SimpleChatContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public void Delete(TKey id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Where(expression);

            return query;
        }

        public TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            entity.CreationDate = DateTime.UtcNow;

            _dbSet.Add(entity);
        }

        public TKey InsertAndGetId(TEntity entity)
        {
            var changeTracking = _dbSet.Add(entity);

            return changeTracking.Entity.Id;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
