using Zql;
using Zql.Dependency;

namespace CompanyName.ProjectName
{
    /// <summary>
    /// Application module
    /// </summary>
    public class ProjectNameApplicationModule : CoreModule
    {
        /// <inheritdoc />
        public override void Load(IocRegisterBase iocRegister)
        {
            iocRegister.RegisterAssemblyByBasicInterface(typeof(ProjectNameApplicationModule).Assembly);
        }
    }
}
