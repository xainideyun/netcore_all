using HT.Future.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HT.Future.IService
{
    public interface IRoleService : IBaseService<Role>
    {
        /// <summary>
        /// 获取用户菜单权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<object> GetUserMenuAuthAsync(int userId);
        /// <summary>
        /// 绑定用户角色关系
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        Task<List<RoleUser>> AddRoleUsersAsync(IEnumerable<RoleUser> roleUsers);
    }
}
