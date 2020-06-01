using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HT.Future.Application;
using HT.Future.Entities;
using HT.Future.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HT.Future.TxApi.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        public UserController(IMapper mapper, IUserService service) : base(mapper)
        {
            this._service = service;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UserDto>>> GetList()
        {
            var list = await _service.TableNoTracking.ToListAsync();
            return _mapper.Map<List<User>, List<UserDto>>(list);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(int id)
        {
            await _service.DeleteAsync(new User { Id = id });
            return Content("删除成功");
        }

        /// <summary>
        /// 获取当前登陆人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<ApiResult<object>> GetCurrentUser()
        {
            await Task.CompletedTask;
            return new { UserId, Name, UserName, IsAdmin };
        }

        /// <summary>
        /// 获取用户绑定的角色
        /// </summary>
        /// <returns></returns>
        [HttpGet("role")]
        public async Task<List<Role>> GetRoles()
        {
            return await _service.GetRolesAsync(UserId);
        }

    }
}