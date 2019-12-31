using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HT.Future.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using HT.Future.Common;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using AutoMapper;
using HT.Future.Entities;
using System.Text;
using NLog.Extensions.Logging;

namespace HT.Future.TxApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                // 设置默认的json格式化参数
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";                 // 日期格式化规范
                    //option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;           // 控制忽略
                    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;     // 循环引用忽略
                });

            #region 跨域

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            #endregion

            #region NLog

            NLog.LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("sqlserver");

            #endregion

            #region Database

            services.AddDbContext<HtDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("sqlserver"), a => a.MigrationsAssembly(typeof(HtDbContext).Assembly.FullName)));

            #endregion

            #region Jwt

            var jwtOption = Configuration.GetSection("JwtSettings").Get<JwtOption>();
            services.AddSingleton(jwtOption);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.SaveToken = true;
                    option.RequireHttpsMetadata = false;
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOption.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOption.Audience,
                        RequireExpirationTime = true,
                        ClockSkew = new TimeSpan(0, 0, Convert.ToInt32(jwtOption.ExpireSeconds)),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtOption.SecurityKey.ToBytes())
                    };
                });

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Netcore通用解决方案",
                    Version = "v1",
                });

                // 将Swagger设置为按照生成的XML文件显示的格式
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // 为swagger添加jwt验证
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "请输入bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference()
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            }, Array.Empty<string>()
                        }
                    });

            });

            #endregion

            #region Mapper

            services.AddAutoMapper(typeof(CustomProfile).Assembly);

            #endregion

            #region 服务注入

            services.AddServices();

            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory factory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseCustomExceptionHandler();

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Netcore通用解决方案");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);    // 跨域中间件

            app.UseAuthentication();                // 认证中间件

            app.UseAuthorization();                 // 授权中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
