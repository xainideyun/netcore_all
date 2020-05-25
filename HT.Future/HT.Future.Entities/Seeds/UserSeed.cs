using HT.Future.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Entities.Seeds
{
    /// <summary>
    /// 用户种子数据
    /// </summary>
    public class UserSeed : ISeed
    {
        public void SetSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "admin", Age = 21, FullName = "超级管理员", IsAdmin = true, PasswordHash = "000000".ToMd5(), Avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png", CreateTime = DateTime.Now });

            modelBuilder.Entity<Address>().HasData(new Address { Id = 1, Lat = 1, Lng = 2, Title = "武汉大学", Detail = "湖北省武汉市珞瑜路12号", UserId = 1, Phone = "13900000000" });

        }
    }
}
