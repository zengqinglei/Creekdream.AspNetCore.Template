using Creekdream;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// ProjectName core module extension methods for <see cref="AppOptionsBuilder" />.
    /// </summary>
    public static class ProjectNameCoreOptionsBuilderExtension
    {
        /// <summary>
        /// Add a ProjectName core module
        /// </summary>
        public static AppOptionsBuilder AddProjectNameCore(this AppOptionsBuilder builder)
        {
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(ProjectNameCoreOptionsBuilderExtension).Assembly);
            return builder;
        }
    }
}
