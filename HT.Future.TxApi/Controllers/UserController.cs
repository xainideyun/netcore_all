﻿using System;
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
    [ApiResultFilter]
    [Authorize]
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

    }
}