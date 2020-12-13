using DatabaseLayer.Configurations.Persons;
using DatabaseLayer.Models.Persons;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PersonEFConfiguration());
        }

        public DbSet<Person> Persons { get; set; }
    }
}
