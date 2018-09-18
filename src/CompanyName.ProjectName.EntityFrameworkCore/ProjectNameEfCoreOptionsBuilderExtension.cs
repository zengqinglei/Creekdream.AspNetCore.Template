using Creekdream;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// ProjectName efcore module extension methods for <see cref="AppOptionsBuilder" />.
    /// </summary>
    public static class ProjectNameEfCoreOptionsBuilderExtension 
    {
        /// <summary>
        /// Add a ProjectName efcore module
        /// </summary>
        public static AppOptionsBuilder AddProjectNameEfCore(this AppOptionsBuilder builder)
        {
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(ProjectNameEfCoreOptionsBuilderExtension).Assembly);
            return builder;
        }
    }
}
