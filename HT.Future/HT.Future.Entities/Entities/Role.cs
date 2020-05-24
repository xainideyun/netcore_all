using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HT.Future.Entities
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("Role")]
    public class Role: BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 角色创建人id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色创建人
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// 是否可以被删除
        /// </summary>
        public bool CanDelete { get; set; }
        /// <summary>
        /// 角色与用户的关系
        /// </summary>
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        /// <summary>
        /// 角色与系统功能的关系
        /// </summary>
        public virtual ICollection<SysFuncRole> SysFuncRoles { get; set; }
    }


}
