using CompanyName.ProjectName.Api.Filters;
using CompanyName.ProjectName.Api.Middlewares;
using CompanyName.ProjectName.EntityFrameworkCore;
using CompanyName.ProjectName.Exceptions;
using CompanyName.ProjectName.Migrations;
using Creekdream.AspNetCore;
using Creekdream.Orm;
using Creekdream.Orm.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Hosting;

namespace CompanyName.ProjectName.Api
{
    /// <inheritdoc />
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <inheritdoc />
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options =>
                {
                    options.Filters.Add(typeof(CustomExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddHealthChecks();
            services.AddDbContext<DbContextBase, ProjectNameDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        _configuration.GetConnectionString("Default"),
                        option => option.UseRowNumberForPaging());
                });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory =
                    actionContext =>
                    {
                        var userFriendlyException = new UserFriendlyException(
                            ErrorCode.UnprocessableEntity,
                            "参数输入不正确");
                        actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0).ToList()
                            .ForEach(
                                e =>
                                {
                                    userFriendlyException.Errors.Add(e.Key, e.Value.Errors.Select(v => v.ErrorMessage));
                                });
                        throw userFriendlyException;
                    };
            });
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "ProjectName API" });
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    options.IncludeXmlComments(Path.Combine(baseDirectory, "CompanyName.ProjectName.Application.xml"));
                    options.IncludeXmlComments(Path.Combine(baseDirectory, "CompanyName.ProjectName.Api.xml"));
                });

            services.AddCreekdream(
                options =>
                {
                    options.UseEfCore();
                    options.AddProjectNameCore();
                    options.AddProjectNameEfCore();
                    options.AddProjectNameApplication();
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app)
        {
            app.UseCreekdream();

            SeedData.Initialize(app.ApplicationServices).Wait();

            if (_webHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRequestLog();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "简单示例项目API");
                });
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
