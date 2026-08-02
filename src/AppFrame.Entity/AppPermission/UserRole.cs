using AppFrame.Entity.EntityBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.AppPermission
{
    public class UserRole : AppEntityBase
    {
        public UserRole()
        {

        }
        public UserRole(int userId, int roleId, DateTime createTime, DateTime updateTime)
        {
            UserId = userId;
            RoleId = roleId;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }

        //[SugarColumn(IsPrimaryKey = true)]
        public int UserId { get; set; }
        //[SugarColumn(IsPrimaryKey = true)]
        public int RoleId { get; set; }
    }
}
