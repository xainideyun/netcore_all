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

        [HttpPost]
        public async Task<ApiResult<Role>> Add([FromBody]JObject obj)
        {
            var role = new Role { CanDelete = true, CreateTime = DateTime.Now, Name = obj["name"].Value<string>() };
            await _service.AddAsync(role);
            return role;
        }

        [HttpPost("bind")]
        public async Task<ApiResult> BindRole([FromBody]List<int> roleIds)
        {
            var arr = roleIds.Select(a => new RoleUser { RoleId = a, UserId = UserId });
            await _service.AddRoleUsersAsync(arr);
            return Ok();
        }


    }
}