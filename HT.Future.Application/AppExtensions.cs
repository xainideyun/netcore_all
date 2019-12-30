using HT.Future.Entities;
using HT.Future.IService;
using HT.Future.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HT.Future.Application
{
    public static class AppExtensions
    {
        //public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddDbContext<HtDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        //}
        
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services)
        {
            var interfaceTypes = typeof(IBaseService<>).Assembly.GetTypes().Where(a => !a.IsGenericType);
            var classTypes = typeof(BaseService<>).Assembly.GetTypes();
            foreach (var item in interfaceTypes)
            {
                var type = classTypes.FirstOrDefault(a => a.GetInterface(item.FullName) != null);
                if (type == null) continue;
                services.AddScoped(item, type);
            }

        }

        /// <summary>
        /// 数据库迁移
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost Migration(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetService<HtDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw new Exception("数据库自动迁移失败", ex);
                }
            }
            return host;
        }
    }
}
