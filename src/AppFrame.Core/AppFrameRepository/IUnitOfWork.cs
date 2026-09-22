using AppFrame.Entity.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameRepository
{
    public interface IUnitOfWork: IDisposable
    {
        IAppRepository<TEntity>  GetRepository<TEntity>() where TEntity : AppEntityBase, new();  // 仓储实现单元
    }
}
