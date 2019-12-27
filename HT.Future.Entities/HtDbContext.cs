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

        }

    }
}
