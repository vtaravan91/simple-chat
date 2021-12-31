using SimpleChat.DataAccess.Entities.Base;
using System;
using System.Threading.Tasks;

namespace SimpleChat.DataAccess.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity, TKey> Repository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : struct;
        Task SaveAsync();
    }
}
