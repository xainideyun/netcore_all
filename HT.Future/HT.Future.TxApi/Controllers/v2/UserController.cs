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

namespace HT.Future.TxApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
            list.Add(new User { Age = 22, FullName = "华天晓" });
            return _mapper.Map<List<User>, List<UserDto>>(list);
        }




    }
}