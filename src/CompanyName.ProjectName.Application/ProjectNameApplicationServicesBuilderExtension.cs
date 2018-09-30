using Creekdream;

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
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(ProjectNameApplicationServicesBuilderExtension).Assembly);
            return builder;
        }
    }
}
