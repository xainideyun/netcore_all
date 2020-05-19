using HT.Future.Common;
using Microsoft.EntityFrameworkCore;
using System;

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


            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "super_admin", Age = 21, FullName = "超级管理员", PasswordHash = "000000".ToMd5(), Avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png" });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, UserName = "admin", Age = 34, FullName = "管理员", PasswordHash = "000000".ToMd5(), Avator = "https://avatars0.githubusercontent.com/u/20942571?s=460&v=4" });

            modelBuilder.Entity<Address>().HasData(new Address { Id = 1, Lat = 1, Lng = 2, Title = "武汉大学", Detail = "湖北省武汉市珞瑜路12号", UserId = 1, Phone = "13900000000" });
        }

    }
}
