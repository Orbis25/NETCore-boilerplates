using DatabaseLayer.Configurations.Persons;
using DatabaseLayer.Models.Base;
using DatabaseLayer.Models.Persons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<BaseModel>())
            {
                switch (entity.State)
                {
                    case EntityState.Modified:
                        entity.Entity.UpdateAt = DateTime.Now;
                        break;
                    case EntityState.Added:
                        entity.Entity.CreatedAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public virtual DbSet<Person> Persons { get; set; }
    }
}
