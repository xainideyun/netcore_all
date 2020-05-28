using HT.Future.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HT.Future.Entities
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("User")]
    public class User : IdentityUser<int>, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get => base.Id; set => base.Id = value; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户全名
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public GenderType Gender { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string Avator { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 地址列表
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; }
        ///// <summary>
        ///// 用户所绑定的角色关系
        ///// </summary>
        //public virtual ICollection<RoleUser> RoleUsers { get; set; }
        /// <summary>
        /// 可访问的路由
        /// </summary>
        public virtual ICollection<AccessAuthority> AccessAuthorities { get; set; }
    }

    /// <summary>
    /// Fluent Api
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);       // 用户名必填且长度最大100
        }
    }

}
