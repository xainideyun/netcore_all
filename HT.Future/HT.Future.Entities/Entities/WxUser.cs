using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Entities
{
    public class WxUser: BaseEntity
    {
        /// <summary>
        /// 用户年龄
        /// </summary>
        public int Age { get; set; } = 0;
        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 用户国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public GenderType Gender { get; set; }
        /// <summary>
        /// 常用语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 微信唯一标识码
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        //public string MyProperty { get; set; }
    }
}
