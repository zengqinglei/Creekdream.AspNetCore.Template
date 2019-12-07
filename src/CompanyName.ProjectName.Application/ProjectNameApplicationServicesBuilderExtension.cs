using Creekdream;
using Creekdream.Dependency;
using AutoMapper;
using CompanyName.ProjectName.MapperProfiles;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// ProjectName application module extension methods for <see cref="ServicesBuilderOptions" />.
    /// </summary>
    public static class ProjectNameApplicationServicesBuilderExtension
    {
        /// <summary>
        /// Add a ProjectName application module
        /// </summary>
        public static ServicesBuilderOptions AddProjectNameApplication(this ServicesBuilderOptions builder)
        {
            builder.Services.AddAutoMapper(typeof(BookProfile));
            builder.Services.RegisterAssemblyByBasicInterface(typeof(ProjectNameApplicationServicesBuilderExtension).Assembly);
            return builder;
        }
    }
}
