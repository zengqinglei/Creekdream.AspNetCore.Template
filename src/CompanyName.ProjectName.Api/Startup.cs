using CompanyName.ProjectName.Api.Filters;
using CompanyName.ProjectName.Api.Middlewares;
using CompanyName.ProjectName.EntityFrameworkCore;
using CompanyName.ProjectName.Exceptions;
using CompanyName.ProjectName.Migrations;
using Creekdream.AspNetCore;
using Creekdream.Dependency;
using Creekdream.Mapping;
using Creekdream.Orm;
using Creekdream.Orm.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;

namespace CompanyName.ProjectName.Api
{
    /// <inheritdoc />
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <inheritdoc />
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(typeof(CustomExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                    options.SwaggerDoc("v1", new Info { Version = "v1", Title = "ProjectName API" });
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    options.IncludeXmlComments(Path.Combine(baseDirectory, "CompanyName.ProjectName.Application.xml"));
                    options.IncludeXmlComments(Path.Combine(baseDirectory, "CompanyName.ProjectName.Api.xml"));
                });

            return services.AddCreekdream(
                options =>
                {
                    options.UseAutofac();
                    options.UseEfCore();
                    options.AddProjectNameCore();
                    options.AddProjectNameEfCore();
                    options.AddProjectNameApplication();
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCreekdream(
                options =>
                {
                    options.UseAutoMapper();
                });

            SeedData.Initialize(app.ApplicationServices).Wait();

            loggerFactory.AddLog4Net();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseMvc();
        }
    }
}
