using DatabaseLayer.Configurations.Base;
using DatabaseLayer.Models.Persons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations.Persons
{
    public class PersonEFConfiguration : BaseEFConfiguration<Person>
    {
        public override void ConfigureEF(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
