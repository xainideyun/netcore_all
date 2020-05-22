using HT.Future.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace HT.Future.Entities
{
    public class HtDbContext : DbContext
    {
        public HtDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);

            // 初始化用户
            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "super_admin", Age = 21, FullName = "超级管理员", IsAdmin = true, PasswordHash = "000000".ToMd5(), Avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png" });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, UserName = "admin", Age = 34, FullName = "管理员", IsAdmin = true, PasswordHash = "000000".ToMd5(), Avator = "https://avatars0.githubusercontent.com/u/20942571?s=460&v=4" });
            modelBuilder.Entity<User>().HasData(new User { Id = 3, UserName = "zhangsan", Age = 34, FullName = "张三", PasswordHash = "000000".ToMd5(), Avator = "https://avatars0.githubusercontent.com/u/20942571?s=460&v=4" });

            modelBuilder.Entity<Address>().HasData(new Address { Id = 1, Lat = 1, Lng = 2, Title = "武汉大学", Detail = "湖北省武汉市珞瑜路12号", UserId = 1, Phone = "13900000000" });

            // 初始化系统功能
            //modelBuilder.Entity<SysFunc>().HasData(
            //    new SysFunc { Id = 1, Name = "good", Title = "商品管理" },
            //    new SysFunc { Id = 2, Name = "settings", Title = "系统设置" },
            //    new SysFunc { Id = 3, Name = "goodList", Title = "商品列表", ParentId = 1 },
            //    new SysFunc { Id = 4, Name = "goodDetail", Title = "商品详情", ParentId = 1 },
            //    new SysFunc { Id = 5, Name = "user", Title = "个人中心", ParentId = 2 },
            //    new SysFunc { Id = 6, Name = "sys", Title = "配置", ParentId = 2 }
            //);

            //// 初始化角色信息
            //modelBuilder.Entity<Role>().HasData(
            //    new Role
            //    {
            //        Id = 1,
            //        Name = "管理员",
            //        CanDelete = false,
            //        RoleUsers = new List<RoleUser>
            //        {
            //            new RoleUser{ Id = 1, UserId = 1 },
            //            new RoleUser{ Id = 2, UserId = 2 },
            //        },
            //        SysFuncRoles = new List<SysFuncRole> 
            //        { 
            //            new SysFuncRole{ Id = 1, SysFuncId = 1 },
            //            new SysFuncRole{ Id = 2, SysFuncId = 2 },
            //            new SysFuncRole{ Id = 3, SysFuncId = 3 },
            //            new SysFuncRole{ Id = 4, SysFuncId = 4 },
            //            new SysFuncRole{ Id = 5, SysFuncId = 5 },
            //            new SysFuncRole{ Id = 6, SysFuncId = 6 }
            //        }
            //    },
            //    new Role
            //    {
            //        Id = 2,
            //        Name = "普通用户",
            //        CanDelete = true,
            //        RoleUsers = new List<RoleUser>
            //        {
            //            new RoleUser{ Id = 3, UserId = 3 }
            //        },
            //        SysFuncRoles = new List<SysFuncRole>
            //        {
            //            new SysFuncRole{ Id = 7, SysFuncId = 1 },
            //            new SysFuncRole{ Id = 8, SysFuncId = 2 },
            //            new SysFuncRole{ Id = 9, SysFuncId = 3 },
            //            new SysFuncRole{ Id = 10, SysFuncId = 4 }
            //        }
            //    }
            //);


        }

    }
}
