using CompanyName.ProjectName.Books;
using CompanyName.ProjectName.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Migrations
{
    /// <summary>
    /// Initialize the data required by the application
    /// </summary>
    public class SeedData
    {
        /// <inheritdoc />
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DbContext>() as ProjectNameDbContext;
                context.Database.Migrate();

                if (await context.Books.CountAsync() < 100)
                {

                    for (int i = 0; i < 100; i++)
                    {
                        var book = new Book
                        {
                            Name = $"Book-{i}",
                            CreationTime = DateTime.Now
                        };
                        await context.Books.AddAsync(book);
                    }

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
