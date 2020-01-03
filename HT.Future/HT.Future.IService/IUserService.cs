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
    }
}
