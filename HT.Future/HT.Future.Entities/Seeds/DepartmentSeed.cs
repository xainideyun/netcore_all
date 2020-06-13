using HT.Future.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Entities.Seeds
{
    /// <summary>
    /// 组织架构种子数据
    /// </summary>
    public class DepartmentSeed : ISeed
    {
        public void SetSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                    new Department { Id = 1, Name = "公司", Description = "我的公司", Sort = 1 },
                    new Department { Id = 2, Name = "人事部", Description = "公司人事部", Sort = 1, ParentId = 1 },
                    new Department { Id = 3, Name = "财务部", Description = "公司财务部", Sort = 2, ParentId = 1 },
                    new Department { Id = 4, Name = "技术部", Description = "公司技术部", Sort = 3, ParentId = 1 }
                );
        }
    }
}
