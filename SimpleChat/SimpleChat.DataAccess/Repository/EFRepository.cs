using Microsoft.EntityFrameworkCore;
using SimpleChat.DataAccess.DBContext;
using SimpleChat.DataAccess.Entities.Base;
using SimpleChat.DataAccess.Repository.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            entity.CreationDate = DateTime.UtcNow;

            await _dbSet.AddAsync(entity);
        }

        public async Task<TKey> InsertAndGetIdAsync(TEntity entity)
        {
            var changeTracking = await _dbSet.AddAsync(entity);

            return changeTracking.Entity.Id;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
