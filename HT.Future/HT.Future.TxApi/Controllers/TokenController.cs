﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HT.Future.Application;
using HT.Future.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HT.Future.Common;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using HT.Future.Entities;

namespace HT.Future.TxApi.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : BaseController
    {
        private IUserService _service;
        private JwtOption _jwtOption;
        private ILogger<TokenController> _logger;
        public TokenController(IMapper mapper, IUserService service, JwtOption jwtOption, ILogger<TokenController> log) : base(mapper)
        {
            _service = service;
            _jwtOption = jwtOption;
            _logger = log;
        }

        /// <summary>
        /// 应用授权（pc端）
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<object>> GetToken(string username, string password)
        {
            var user = await _service.GetByUserNameAsync(username);
            if (user == null)
            {
                return BadRequest("用户不存在");
            }
            if (user.Password != password.ToMd5())
            {
                return BadRequest("密码不正确");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FullName),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString().ToLower()),
                new Claim(JwtRegisteredClaimNames.Aud, user.IsAdmin ? "1" : "0")
            };
            var signKey = new SymmetricSecurityKey(_jwtOption.SecurityKey.ToBytes());
            var token = new JwtSecurityToken(_jwtOption.Issuer, _jwtOption.Audience, claims, expires: DateTime.Now.AddSeconds(_jwtOption.ExpireSeconds), signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256));

            _logger.LogInformationAsync($"用户{user.FullName}于{DateTime.Now:yyyy-MM-dd HH:mm:ss}登录");
            return new { token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        /// <summary>
        /// 应用授权（小程序端）
        /// </summary>
        /// <returns></returns>
        [HttpGet("code/{code}")]
        public async Task<ApiResult<object>> GetToken(string code)
        {
            return new { name = "孙小双", age = 11, code};
        }

        /// <summary>
        /// 根据openid获取token
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet("open")]
        public async Task<ApiResult<object>> GetTokenByOpenId(string openid)
        {
            return null;
        }


        [HttpGet("user")]
        public async Task<ApiResult<UserDto>> GetUser([FromQuery]string username, [FromQuery]string password, [FromQuery]string name)
        {
            username ??= "admin";
            password ??= "000000";
            name ??= username;
            var user = await _service.GetByUserNameAsync(username);
            if (user == null)
            {
                user = new User { UserName = username, Age = 18, FullName = name, IsAdmin = true, Password = password.ToMd5(), Avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png", CreateTime = DateTime.Now, DepartmentId = 1 };
                await _service.AddAsync(user);
            }
            return _mapper.Map<User, UserDto>(user);

        }

    }
}