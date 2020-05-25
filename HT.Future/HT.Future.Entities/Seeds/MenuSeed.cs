using HT.Future.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Entities.Seeds
{
    public class MenuSeed : ISeed
    {
        public void SetSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 1, Name = "good", Title = "商品管理" },
                new Menu { Id = 2, Name = "settings", Title = "系统设置" },
                new Menu { Id = 3, Name = "goodList", Title = "商品列表", ParentId = 1 },
                new Menu { Id = 4, Name = "goodDetail", Title = "商品详情", ParentId = 1 },
                new Menu { Id = 5, Name = "user", Title = "个人中心", ParentId = 2 },
                new Menu { Id = 6, Name = "order", Title = "订单管理" },
                new Menu { Id = 7, Name = "sys", Title = "配置", ParentId = 2 },
                new Menu { Id = 8, Name = "myOrder", Title = "我的订单", ParentId = 6 }
            );

        }
    }
}
