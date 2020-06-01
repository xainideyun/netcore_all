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
        /// 获取系统所有角色
        /// </summary>
        /// <returns></returns>
        Task<List<Role>> GetAllRolesAsync();
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<Role> UpdateRoleAsync(Role role);
        /// <summary>
        /// 新增用户角色关系
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        Task<List<RoleUser>> AddRoleUsersAsync(IEnumerable<RoleUser> users);
    }
}
