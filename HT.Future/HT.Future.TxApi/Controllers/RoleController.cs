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
using Newtonsoft.Json.Linq;

namespace HT.Future.TxApi.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/[controller]")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _service;

        public RoleController(IMapper mapper, IRoleService service) : base(mapper)
        {
            this._service = service;
        }

        /// <summary>
        /// 获取系统所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<Role>>> GetRoles()
        {
            return await _service.GetAllRolesAsync();
        }

        /// <summary>
        /// 新增一个角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<Role>> Add([FromBody]Role role)
        {
            role.UserId = UserId;
            role.CreateTime = DateTime.Now;
            await _service.AddAsync(role);
            return role;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ApiResult<Role>> Update(int id, [FromBody]Role role)
        {
            await _service.UpdateRoleAsync(role);
            return role;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(int id)
        {
            var role = new Role { Id = id };
            //_service.Attach(role);
            await _service.DeleteAsync(role);
            return Ok();
        }

        /// <summary>
        /// 为当前用户绑定角色
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost("bind")]
        public async Task<ApiResult> BindRole([FromBody]List<int> roleIds)
        {
            var arr = roleIds.Select(a => new RoleUser { RoleId = a, UserId = UserId });
            await _service.AddRoleUsersAsync(arr);
            return Ok();
        }




    }
}