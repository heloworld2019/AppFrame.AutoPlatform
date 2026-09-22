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
    public class AppUser: AppEntityBase
    {
        public AppUser()
        {

        }
        public AppUser(string userName, string account, string password, DateTime createTime, DateTime updateTime)
        {
            UserName = userName;
            Account = account;
            Password = password;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }

        [AppRequired(ErrorInfo = "用户名不能为空")]
        public string UserName { get; set; }
        [AppRequired(ErrorInfo = "账号不能为空")]
        public string Account { get; set; }
        [AppRequired(ErrorInfo = "密码不能为空")]
        public string Password { get; set; }
        [Navigate(typeof(UserRole), nameof(UserRole.UserId), nameof(UserRole.RoleId))] //  中间表实体类型 ，中间表里的本实体Id字段名（外键），中间表里关联的目标实体Id字段名 （外键)
        public List<AppRole> Roles { get; set; }
    }
}
