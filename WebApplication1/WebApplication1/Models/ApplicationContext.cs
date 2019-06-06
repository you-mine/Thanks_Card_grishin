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
        public Dbset<Place> Place { get; set; }
        public Dbset<Helps1> Helps1 { get; set; }
        public Dbset<Helps2> Helps2 { get; set; }

    }
}