using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HT.Future.Entities
{
    /// <summary>
    /// 系统功能表
    /// </summary>
    [Table("SysFunc")]
    public class SysFunc : BaseEntity
    {
        /// <summary>
        /// 路由名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路由标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 父级路由的id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 父级路由实体
        /// </summary>
        public virtual SysFunc Parent { get; set; }
        public virtual ICollection<SysFuncRole> SysFuncRoles { get; set; }
    }
}
