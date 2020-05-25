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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace HT.Future.TxApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();     // 移除默认映射
        }

        public IConfiguration Configuration { get; }
        public IApiVersionDescriptionProvider ApiProvider { get; set; }

        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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


            #region api版本

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;                         // 可选，为true时API返回支持的版本信息
                o.AssumeDefaultVersionWhenUnspecified = true;       // 不提供版本时，使用默认版本
                o.DefaultApiVersion = new ApiVersion(1, 0);         // 请求中未指定版本时默认为1.0
            }).AddVersionedApiExplorer(option =>
            {
                // 版本名的格式：v+版本号
                option.GroupNameFormat = "'v'V";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
            using var scope = services.BuildServiceProvider().CreateScope();
            ApiProvider = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

            #endregion

            #region 跨域

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins,
            //    builder =>
            //    {
            //        builder.WithOrigins("*")
            //        .AllowAnyHeader()
            //        .AllowAnyMethod();
            //    });
            //});

            #endregion

            #region NLog

            //NLog.LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("sqlserver");
            NLog.LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("mysql");

            #endregion

            #region Database

            //services.AddDbContext<HtDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("mysql"), a => a.MigrationsAssembly(typeof(HtDbContext).Assembly.FullName)));

            services.AddDbContext<HtDbContext>(options => options.UseMySQL(Configuration["ConnectionStrings:mysql"], b => b.MigrationsAssembly(typeof(HtDbContext).Assembly.FullName)));
            //services.Migration();

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
                        ClockSkew = new TimeSpan(0, 0, jwtOption.ExpireSeconds),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtOption.SecurityKey.ToBytes())
                    };
                });
            //services.AddAuthorization(option =>
            //{
            //    option.AddPolicy("Admin", policy => policy.Requirements.Add(new PolicyRequirement()));
            //});

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                foreach (var item in ApiProvider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(item.GroupName, new OpenApiInfo
                    {
                        Title = "Netcore解决方案",
                        Version = item.ApiVersion.ToString(),
                    });
                }

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
                        { new OpenApiSecurityScheme {
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCustomExceptionHandler();
            }

            app.UseCustomExceptionHandler();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                foreach (var item in ApiProvider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", "CoreAPI" + item.ApiVersion);
                }
                //c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            //app.UseCors(MyAllowSpecificOrigins);    // 跨域中间件

            app.UseAuthentication();                // 开启认证中间件

            app.UseAuthorization();                 // 授权中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
