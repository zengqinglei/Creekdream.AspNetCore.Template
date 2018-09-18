using CompanyName.ProjectName.Books;
using Microsoft.EntityFrameworkCore;
using Creekdream.Orm.EntityFrameworkCore;

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
        /// Books
        /// </summary>
        public DbSet<Book> Books { get; set; }
    }
}
