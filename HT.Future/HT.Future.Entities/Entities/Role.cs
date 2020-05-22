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
        /// 是否可以被删除
        /// </summary>
        public bool CanDelete { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        public virtual ICollection<SysFuncRole> SysFuncRoles { get; set; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(a => a.RoleUsers)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.SysFuncRoles)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.Role)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
