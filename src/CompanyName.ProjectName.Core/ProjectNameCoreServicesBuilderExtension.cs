using Creekdream;
using Creekdream.Dependency;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// ProjectName core module extension methods for <see cref="ServicesBuilderOptions" />.
    /// </summary>
    public static class ProjectNameCoreServicesBuilderExtension
    {
        /// <summary>
        /// Add a ProjectName core module
        /// </summary>
        public static ServicesBuilderOptions AddProjectNameCore(this ServicesBuilderOptions builder)
        {
            builder.Services.RegisterAssemblyByBasicInterface(typeof(ProjectNameCoreServicesBuilderExtension).Assembly);
            return builder;
        }
    }
}
