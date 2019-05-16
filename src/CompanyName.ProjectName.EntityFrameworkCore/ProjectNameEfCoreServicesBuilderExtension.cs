using Creekdream;
using Creekdream.Dependency;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// ProjectName efcore module extension methods for <see cref="ServicesBuilderOptions" />.
    /// </summary>
    public static class ProjectNameEfCoreServicesBuilderExtension
    {
        /// <summary>
        /// Add a ProjectName efcore module
        /// </summary>
        public static ServicesBuilderOptions AddProjectNameEfCore(this ServicesBuilderOptions builder)
        {
            builder.Services.RegisterAssemblyByBasicInterface(typeof(ProjectNameEfCoreServicesBuilderExtension).Assembly);
            return builder;
        }
    }
}
