using Creekdream;

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
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(ProjectNameCoreServicesBuilderExtension).Assembly);
            return builder;
        }
    }
}
