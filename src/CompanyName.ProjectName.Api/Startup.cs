using CompanyName.ProjectName.Api.Filters;
using CompanyName.ProjectName.Api.Middlewares;
using CompanyName.ProjectName.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
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

            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new Info { Version = "v1", Title = "ProjectName API" });

                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var currentNamespace = typeof(ProjectNameApplicationModule).Namespace;
                    var xmlFiles = new[]
                    {
                        string.Format("{0}/{1}.Application.xml", baseDirectory, currentNamespace),
                        string.Format("{0}/{1}.Api.xml", baseDirectory, currentNamespace)
                    };
                    foreach (var xmlFile in xmlFiles)
                    {
                        if (File.Exists(xmlFile))
                        {
                            options.IncludeXmlComments(xmlFile);
                        }
                    }
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCustomRewriter();
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
