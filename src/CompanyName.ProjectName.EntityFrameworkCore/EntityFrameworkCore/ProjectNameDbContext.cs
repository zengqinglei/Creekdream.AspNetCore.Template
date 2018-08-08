using Microsoft.EntityFrameworkCore;

namespace CompanyName.ProjectName.EntityFrameworkCore
{
    /// <summary>
    /// ProjectName database access context
    /// </summary>
    public class ProjectNameDbContext : DbContext
    {
        public ProjectNameDbContext(DbContextOptions<ProjectNameDbContext> options)
            : base(options)
        {

        }
    }
}
