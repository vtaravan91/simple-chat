using Autofac;
using SimpleChat.DataAccess.DBContext;
using SimpleChat.DataAccess.Entities.Base;
using SimpleChat.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;

namespace SimpleChat.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDictionary<string, object> _repositories;

        private readonly SimpleChatContext _context;
        private readonly ILifetimeScope _scope;

        public IRepository<TEntity, TKey> Repository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : struct
        {
            var typeName = typeof(TEntity).Name;

            if (_repositories.ContainsKey(typeName))
            {
                return (IRepository<TEntity, TKey>)_repositories[typeName];
            }

            var parameter = new TypedParameter(typeof(SimpleChatContext), _context);
            var repository = _scope.Resolve<IRepository<TEntity, TKey>>(parameter);

            _repositories.Add(typeName, repository);

            return repository;
        }

        public UnitOfWork(ILifetimeScope scope)
        {
            _context = scope.Resolve<SimpleChatContext>();
            _scope = scope;
            _repositories = new Dictionary<string, object>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
