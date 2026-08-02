using AppFrame.Core.AppFrameRepository;
using AppFrame.Entity.AppPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameRepository.Repository
{
    public class PermissionRepository : RepositoryBase<AppPermission>, IAppRepository<AppPermission>
    {
    }
}
