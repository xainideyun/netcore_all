using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HT.Future.Entities
{
    /// <summary>
    /// 权限与角色关联表
    /// </summary>
    [Table("SysFuncRole")]
    public class SysFuncRole : BaseEntity
    {
        public int SysFuncId { get; set; }
        public virtual SysFunc SysFunc { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
