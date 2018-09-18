using Creekdream;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// ProjectName application module extension methods for <see cref="AppOptionsBuilder" />.
    /// </summary>
    public static class ProjectNameApplicationOptionsBuilderExtension
    {
        /// <summary>
        /// Add a ProjectName application module
        /// </summary>
        public static AppOptionsBuilder AddProjectNameApplication(this AppOptionsBuilder builder)
        {
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(ProjectNameApplicationOptionsBuilderExtension).Assembly);
            return builder;
        }
    }
}
