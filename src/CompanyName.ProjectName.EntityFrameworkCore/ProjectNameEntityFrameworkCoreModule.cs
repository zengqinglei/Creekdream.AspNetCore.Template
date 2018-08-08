using Zql;
using Zql.Dependency;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// EntityFrameworkCore module
    /// </summary>
    public class ProjectNameEntityFrameworkCoreModule : CoreModule
    {
        /// <inheritdoc />
        public override void Load(IocRegisterBase iocRegister)
        {
            iocRegister.RegisterAssemblyByBasicInterface(typeof(ProjectNameEntityFrameworkCoreModule).Assembly);
        }
    }
}
