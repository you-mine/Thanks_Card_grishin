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
        public DbSet<Help1Content> help1Content { get; set; }
        public DbSet<Help2Content> help2Content { get; set; }
        public DbSet<PlaceContent> placeContent { get; set; }

    }
}