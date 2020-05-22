using AutoMapper;
using HT.Future.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HT.Future.Application
{
    [Produces("application/json")]
    [ApiController]
    //[ApiResultFilter]
    public class BaseController : ControllerBase
    {
        protected IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        private int userId;
        /// <summary>
        /// 当前登陆者id
        /// </summary>
        public int UserId
        {
            get
            {
                if (userId > 0) return userId;
                var claim = User.FindFirst(a => a.Type == JwtRegisteredClaimNames.NameId);
                if (claim == null)
                {
                    throw new AppException(ApiResultStatusCode.UnAuthorized, "用户未登录或登录已过期");
                }
                userId = claim.Value.ToInt();
                return userId;
            }
        }

        private string userName;
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserName
        {
            get
            {
                if (!string.IsNullOrEmpty(userName)) return userName;
                userName = GetJwtClaim(JwtRegisteredClaimNames.Sub);
                return userName;
            }
        }

        private string name;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(name)) return name;
                name = GetJwtClaim(JwtRegisteredClaimNames.GivenName);
                return name;
            }
        }


        private string GetJwtClaim(string field)
        {
            var claim = User.FindFirst(a => a.Type == field);
            if (claim == null)
            {
                throw new AppException(ApiResultStatusCode.UnAuthorized, "用户未登录或登录已过期");
            }
            return claim.Value;
        }

    }
}
