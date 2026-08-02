using AppFrame.Core.AppFrameRepository;
using AppFrame.Entity.EntityBase;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameRepository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private bool _isDisposed = false;
        private ConcurrentDictionary<Type, object> _repositories;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _repositories.Clear();
                }
            }
            _isDisposed = true;
        }

        public IAppRepository<TEntity> GetRepository<TEntity>() where TEntity : AppEntityBase, new()
        {
            if (_repositories == null)
                _repositories = new ConcurrentDictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new RepositoryBase<TEntity>();
            }
            return (IAppRepository<TEntity>)_repositories[type];
        }
    }
}
