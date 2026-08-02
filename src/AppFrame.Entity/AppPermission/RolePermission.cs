using AppFrame.Entity.EntityBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.AppPermission
{
    public class RolePermission : AppEntityBase
    {
        public RolePermission()
        {

        }

        public RolePermission(int roleId, int permissionId, DateTime createTime, DateTime updateTime)
        {
            RoleId = roleId;
            PermissionId = permissionId;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }

        //[SugarColumn(IsPrimaryKey = true)]
        public int RoleId { get; set; }
        //[SugarColumn(IsPrimaryKey = true)]
        public int PermissionId { get; set; }
    }
}
