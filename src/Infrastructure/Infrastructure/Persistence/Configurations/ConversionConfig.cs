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
    public class ConversionConfig : IEntityTypeConfiguration<Conversion>
    {
        public void Configure(EntityTypeBuilder<Conversion> builder)
        {
            builder.ToTable("Conversions");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CurrencyFrom)
                .WithMany(x => x.ConversionsForFrom)
                .HasForeignKey(x => x.CurrencyFromId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.CurrencyTo)
                .WithMany(x => x.ConversionsForTo)
                .HasForeignKey(x => x.CurrencyToId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
