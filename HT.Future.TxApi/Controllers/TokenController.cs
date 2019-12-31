using System;
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
//using NLog;
using Microsoft.Extensions.Logging;

namespace HT.Future.TxApi.Controllers
{
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

        //public ILogger Logger { get => LogManager.GetCurrentClassLogger(); }

        /// <summary>
        /// 应用授权
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
                return Content("用户不存在");
            }
            if (user.PasswordHash != password.ToMd5())
            {
                return Content("密码不正确");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString().ToLower())
            };
            var signKey = new SymmetricSecurityKey(_jwtOption.SecurityKey.ToBytes());
            var token = new JwtSecurityToken(_jwtOption.Issuer, _jwtOption.Audience, claims, expires: DateTime.Now.AddDays(1), signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256));

            _logger.LogWarning($"用户{user.FullName}于{DateTime.Now:yyyy-MM-dd HH:mm:ss}登录");
            return new { token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

    }
}