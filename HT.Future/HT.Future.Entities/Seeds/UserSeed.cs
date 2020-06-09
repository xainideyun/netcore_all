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
            //var time = new DateTime(2020, 1, 1, 8, 0, 0);
            //modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "admin", Age = 21, FullName = "超级管理员", IsAdmin = true, Password = "000000".ToMd5(), Avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png", CreateTime = time });

            //modelBuilder.Entity<Address>().HasData(new Address { Id = 1, Lat = 1, Lng = 2, Title = "武汉大学", Detail = "湖北省武汉市珞瑜路12号", UserId = 1, Phone = "13900000000" });


            //modelBuilder.Entity<User>().HasData(new User { Id = 2, UserName = "guanyu", Age = 33, FullName = "关羽", Password = "000000".ToMd5(), Avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png", CreateTime = time });
            //modelBuilder.Entity<User>().HasData(new User { Id = 3, UserName = "zhangfei", Age = 29, FullName = "张飞", Password = "000000".ToMd5(), Avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png", CreateTime = time });
            //modelBuilder.Entity<User>().HasData(new User { Id = 4, UserName = "zhaoyun", Age = 27, FullName = "赵云", Password = "000000".ToMd5(), Avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png", CreateTime = time });

        }
    }
}
