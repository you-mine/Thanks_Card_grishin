using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;

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
        public DbSet<PlaceContent> Place { get; set; }
        public DbSet<Helps1Content> Helps1 { get; set; }
        public DbSet<Helps2Content> Helps2 { get; set; }

    }
}