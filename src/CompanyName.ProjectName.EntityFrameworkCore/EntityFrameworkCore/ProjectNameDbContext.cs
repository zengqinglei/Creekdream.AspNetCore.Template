using CompanyName.ProjectName.Books;
using Microsoft.EntityFrameworkCore;
using Zql.Orm.EntityFrameworkCore;

namespace CompanyName.ProjectName.EntityFrameworkCore
{
    /// <summary>
    /// ProjectName database access context
    /// </summary>
    public class ProjectNameDbContext : DbContextBase
    {
        public ProjectNameDbContext(DbContextOptions<ProjectNameDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// 书信息数据集合
        /// </summary>
        public DbSet<Book> Books { get; set; }
    }
}
