using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HT.Future.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HT.Future.TxApi.Controllers
{
    public class TokenController : BaseController
    {
        public TokenController(IMapper mapper) : base(mapper)
        {
        }

        /// <summary>
        /// 应用授权
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<object>> GetToken(string username, string password)
        {
            await Task.CompletedTask;
            return null;
        }

    }
}