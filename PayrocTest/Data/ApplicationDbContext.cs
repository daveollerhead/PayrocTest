using Microsoft.EntityFrameworkCore;
using PayrocTest.Data.Configuration;
using PayrocTest.Models;

namespace PayrocTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(LinkEntityConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
