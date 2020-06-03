using HT.Future.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HT.Future.IService
{
    public interface IUserService: IBaseService<User>
    {
        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User> GetByUserNameAsync(string username);
        /// <summary>
        /// 获取用户所绑定的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Role>> GetRolesAsync(int userId);
        /// <summary>
        /// 获取用户可访问菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<string>> GetAccessMenuAsync(int userId);

    }
}
