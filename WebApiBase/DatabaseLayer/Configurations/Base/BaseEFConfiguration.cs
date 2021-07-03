using DatabaseLayer.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseLayer.Configurations.Base
{
    public abstract class BaseEFConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseModel
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);
            ConfigureEF(builder);
        }

        public abstract void ConfigureEF(EntityTypeBuilder<TEntity> builder);
    }
}
