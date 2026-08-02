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
    public class AppPermission : AppEntityBase
    {
        public AppPermission()
        {

        }
        public AppPermission(string permissionName, string permissionType, string description, DateTime createTime, DateTime updateTime)
        {
            PermissionName = permissionName;
            PermissionType = permissionType;
            Description = description;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }

        [AppRequired(ErrorInfo = "权限名不能为空")]
        public string PermissionName { get; set; }
        [SugarColumn(IsNullable = true)]
        public string PermissionType { get; set; }
        [SugarColumn(IsNullable = true)]
        public string Description { get; set; }
        [Navigate(typeof(RolePermission), nameof(RolePermission.PermissionId), nameof(RolePermission.RoleId))]
        public List<AppRole> Roles { get; set; }
    }
}
