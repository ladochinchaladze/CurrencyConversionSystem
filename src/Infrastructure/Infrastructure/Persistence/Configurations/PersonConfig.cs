using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.IdentityNumber).IsUnique();
            builder.HasIndex(x => x.RecommendatorIdentityNumber);

            builder.HasOne(x => x.Recomendator)
                .WithMany(x => x.Recommendeds)
                .HasPrincipalKey(x => x.RecommendatorIdentityNumber);

            builder.HasMany(x => x.Conversions)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);
        }
    }
}
