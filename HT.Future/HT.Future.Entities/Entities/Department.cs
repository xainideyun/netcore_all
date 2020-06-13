using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Entities
{
    public class Department: BaseEntity
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 父级组织id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 父级组织实体对象
        /// </summary>
        public virtual Department Parent { get; set; }
        /// <summary>
        /// 主管id
        /// </summary>
        public int? ManagerId { get; set; }
        /// <summary>
        /// 主管实体对象
        /// </summary>
        public virtual User Manager { get; set; }
        /// <summary>
        /// 组织下的用户
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }


    /// <summary>
    /// Fluent Api
    /// </summary>
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(a => a.Users)
                .WithOne(a => a.Department)
                .HasForeignKey(a => a.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
