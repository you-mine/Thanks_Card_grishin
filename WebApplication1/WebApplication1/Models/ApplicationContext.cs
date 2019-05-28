using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ThanksCard> ThanksCards { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}