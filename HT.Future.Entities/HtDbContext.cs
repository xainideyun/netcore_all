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


            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "sunxiaoshuang", Age = 31, FullName = "孙小双", PasswordHash = "000000".ToMd5() });

            modelBuilder.Entity<Address>().HasData(new Address { Id = 1, Lat = 1, Lng = 2, Title = "武汉大学", Detail = "湖北省武汉市珞瑜路12号", UserId = 1, Phone = "13900000000" });
        }

    }
}
