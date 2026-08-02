using AppFrame.Common.AppAttribute.Validate;
using AppFrame.Entity.EntityBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.AppPermission
{
    public class AppRole: AppEntityBase
    {
        public AppRole()
        {

        }

        public AppRole(string roleName, DateTime createTime, DateTime updateTime)
        {
            RoleName = roleName;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }

        [AppRequired(ErrorInfo = "角色名不能为空")]
        public string RoleName { get; set; }
        [Navigate(typeof(RolePermission), nameof(RolePermission.RoleId), nameof(RolePermission.PermissionId))]
        public List<AppPermission> Permissions { get; set; }
        [Navigate(typeof(UserRole), nameof(UserRole.RoleId), nameof(UserRole.UserId))]
        public List<AppUser> Users { get; set; }
    }
}
