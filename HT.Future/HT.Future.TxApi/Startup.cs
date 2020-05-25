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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();     // �Ƴ�Ĭ��ӳ��
        }

        public IConfiguration Configuration { get; }
        public IApiVersionDescriptionProvider ApiProvider { get; set; }

        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                // ����Ĭ�ϵ�json��ʽ������
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";                 // ���ڸ�ʽ���淶
                    //option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;           // ���ƺ���
                    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;     // ѭ�����ú���
                });


            #region api�汾

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;                         // ��ѡ��ΪtrueʱAPI����֧�ֵİ汾��Ϣ
                o.AssumeDefaultVersionWhenUnspecified = true;       // ���ṩ�汾ʱ��ʹ��Ĭ�ϰ汾
                o.DefaultApiVersion = new ApiVersion(1, 0);         // ������δָ���汾ʱĬ��Ϊ1.0
            }).AddVersionedApiExplorer(option =>
            {
                // �汾���ĸ�ʽ��v+�汾��
                option.GroupNameFormat = "'v'V";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
            using var scope = services.BuildServiceProvider().CreateScope();
            ApiProvider = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

            #endregion

            #region ����

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
                        Title = "Netcore�������",
                        Version = item.ApiVersion.ToString(),
                    });
                }

                // ��Swagger����Ϊ�������ɵ�XML�ļ���ʾ�ĸ�ʽ
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Ϊswagger���jwt��֤
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "������bearer",
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

            #region ����ע��

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

            //app.UseCors(MyAllowSpecificOrigins);    // �����м��

            app.UseAuthentication();                // ������֤�м��

            app.UseAuthorization();                 // ��Ȩ�м��

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
