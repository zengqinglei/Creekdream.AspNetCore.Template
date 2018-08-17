using CompanyName.ProjectName.Api.Filters;
using CompanyName.ProjectName.Api.Middlewares;
using CompanyName.ProjectName.EntityFrameworkCore;
using CompanyName.ProjectName.Exceptions;
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
using Zql.AspNetCore;
using Zql.AutoMapper;
using Zql.Dependency.Autofac;

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
            services.AddDbContext<DbContext, ProjectNameDbContext>(
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
                    var currentNamespace = typeof(ProjectNameApplicationModule).Namespace;
                    options.IncludeXmlComments(Path.Combine(baseDirectory, $"{currentNamespace}.Application.xml"));
                    options.IncludeXmlComments(Path.Combine(baseDirectory, $"{currentNamespace}.Api.xml"));
                });
            services.AddAutoMapper();

            return services.AddZql<AutofacIocRegister>(
                config =>
                {

                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
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
