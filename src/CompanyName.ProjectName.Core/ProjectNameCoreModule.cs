using Zql;
using Zql.Dependency;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// Domain module
    /// </summary>
    public class ProjectNameCoreModule : CoreModule
    {
        /// <inheritdoc />
        public override void Load(IocRegisterBase iocRegister)
        {
            iocRegister.RegisterAssemblyByBasicInterface(typeof(ProjectNameCoreModule).Assembly);
        }
    }
}
