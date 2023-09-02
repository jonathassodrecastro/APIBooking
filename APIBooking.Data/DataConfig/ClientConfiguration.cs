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
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.LastName)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(x => x.Age)
               .HasColumnType("smallint")
               .IsRequired();

            builder.Property(x => x.PhoneNumber)
               .HasColumnType("varchar(30)")
               .IsRequired();
        }
    }
}
