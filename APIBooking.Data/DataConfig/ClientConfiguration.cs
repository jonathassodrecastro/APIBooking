using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBooking.Data.DataConfig
{
    public class ClientConfiguration : IEntityTypeConfiguration<EntityClient>
    {
        public void Configure(EntityTypeBuilder<EntityClient> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(k => k.id);
            builder.Property(k => k.id).ValueGeneratedOnAdd();

            builder.Property(x => x.name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.lastname)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(x => x.age)
               .HasColumnType("smallint")
               .IsRequired();

            builder.Property(x => x.phoneNumber)
               .HasColumnType("varchar(30)")
               .IsRequired();
        }
    }
}
