using AppFrame.Core.AppFrameRepository;
using AppFrame.Entity.AppPermission;
using AppFrame.Infrastructrue.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFrame.ConsoleTest.Repository
{
    internal class RepositoryTest
    {
        private readonly AppIoc _appIoc;
        private readonly IAppRepository<AppUser> _userRepository;
        private readonly IAppRepository<AppRole> _roleRepository;
        private readonly IAppRepository<AppPermission> _permissionRepository;
        private readonly IAppRepository<UserRole> _userRoleRepository;
        private readonly IAppRepository<RolePermission> _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RepositoryTest(AppIoc appIoc)
        {
            _appIoc = appIoc;
            _unitOfWork = _appIoc.Resove<IUnitOfWork>();
            _userRepository = _unitOfWork.GetRepository<AppUser>();
            _roleRepository = _unitOfWork.GetRepository<AppRole>();
            _permissionRepository = _unitOfWork.GetRepository<AppPermission>();
            _userRoleRepository = _unitOfWork.GetRepository<UserRole>();
            _rolePermissionRepository = _unitOfWork.GetRepository<RolePermission>();
        }
        public void Insert()
        {
            _userRepository.Insert(new List<AppUser>()
            {
                new AppUser("jyq", "jyq123", "123456", DateTime.UtcNow, DateTime.UtcNow) ,
                new AppUser("rhq", "rhq123", "123456", DateTime.UtcNow, DateTime.UtcNow) ,
            });
            _roleRepository.Insert(new List<AppRole>()
            {
                new AppRole("管理员",DateTime.UtcNow,DateTime.UtcNow),
                new AppRole("工程师",DateTime.UtcNow,DateTime.UtcNow),
                new AppRole("操作员",DateTime.UtcNow,DateTime.UtcNow),
                new AppRole("游客",DateTime.UtcNow,DateTime.UtcNow),
            });
            _userRoleRepository.Insert(new List<UserRole>()
            {
                new UserRole(1,1,DateTime.UtcNow,DateTime.UtcNow),
                new UserRole(2,1,DateTime.UtcNow,DateTime.UtcNow),
            });
            _permissionRepository.Insert(new List<AppPermission>()
            {
                new AppPermission("控制台","","",DateTime.UtcNow,DateTime.UtcNow),
                new AppPermission("设备操作","","",DateTime.UtcNow,DateTime.UtcNow),
                new AppPermission("参数配置","","",DateTime.UtcNow,DateTime.UtcNow),
                new AppPermission("生产统计","","",DateTime.UtcNow,DateTime.UtcNow),
                new AppPermission("日志管理","","",DateTime.UtcNow,DateTime.UtcNow),
                new AppPermission("用户管理","","",DateTime.UtcNow,DateTime.UtcNow),
                new AppPermission("系统配置","","",DateTime.UtcNow,DateTime.UtcNow),
                new AppPermission("版本管理","","",DateTime.UtcNow,DateTime.UtcNow),
                new AppPermission("关于","","",DateTime.UtcNow,DateTime.UtcNow),
            });
            _rolePermissionRepository.Insert(new List<RolePermission>()
            {
                new RolePermission(1,1,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(1,2,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(1,3,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(1,4,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(1,5,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(1,6,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(1,7,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(1,8,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(1,9,DateTime.UtcNow,DateTime.UtcNow),

                new RolePermission(2,1,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(2,2,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(2,3,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(2,4,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(2,5,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(2,8,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(2,9,DateTime.UtcNow,DateTime.UtcNow),

                new RolePermission(3,1,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(3,2,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(3,4,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(3,5,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(3,8,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(3,9,DateTime.UtcNow,DateTime.UtcNow),

                new RolePermission(4,1,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(4,4,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(4,5,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(4,8,DateTime.UtcNow,DateTime.UtcNow),
                new RolePermission(4,9,DateTime.UtcNow,DateTime.UtcNow),
            });
        }

        public void Query()
        {
            var users = _userRepository.QueryAll();
            var roles = _roleRepository.QueryAll();
            var userRoles = _userRoleRepository.QueryAll();
            var permissions = _permissionRepository.QueryAll();
            var rolePermissions = _rolePermissionRepository.QueryAll();
            var user = _userRepository.Query(3);
            if (user == null) 
            {
                Console.WriteLine($"用户不存在");
                return;
            } 
            //根据用户ID获取角色信息
            var roleList = roles.Join(userRoles, r => r.Id, u => u.RoleId, (r, u) => user.Id.Equals(u.UserId) ? r : null).ToList();
            AppRole role = null;
            role = roleList.FirstOrDefault(p => p != null);
            if (role == null)
            {
                Console.WriteLine($"角色不存在");
                return;
            }
            Console.WriteLine($"{user.UserName}的角色：{role.RoleName}");
            Console.WriteLine($"-------------------------");
            Console.WriteLine($"{user.UserName}的权限：");
            var roleIds = roles.Where(p => p.Id.Equals(1)).Select(p => p.Id);
            //根据角色ID获取权限信息
            var permission = permissions.Join(rolePermissions, p => p.Id, c => c.PermissionId, (p, c) => roleIds.Contains(c.RoleId) ? p : null).ToList();
            foreach (var item in permission)
            {
                if (item == null) continue;
                Console.WriteLine(item.PermissionName);
            }
            //根据角色获取权限信息
            var permission2 = permissions.Join(rolePermissions, p => p.Id, c => c.PermissionId, (p, c) => role.Id.Equals(c.RoleId) ? p : null).ToList();
            Console.WriteLine($"-------------------------");
            Console.WriteLine($"{user.UserName}的权限：");
            foreach (var item in permission2)
            {
                if (item == null) continue;
                Console.WriteLine(item.PermissionName);
            }
        }
        public void AddUser()
        {
            var user = new AppUser("pri", "pri_123", "123456", DateTime.UtcNow, DateTime.UtcNow);
            _userRepository.Insert(user);
            //此处需根据返回的user对象ID，创建一个用户角色对象..
            //此处暂时测试模拟
            var userRole = new UserRole(3, 2, DateTime.UtcNow, DateTime.UtcNow);
            _userRoleRepository.Insert(userRole);
        }
        public void UpdateUser()
        {
            //直接更新即可

        }
        public void RemoveUser()
        {
            //删除user后，删除userRole
        }
    }
}
