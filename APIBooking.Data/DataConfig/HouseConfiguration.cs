using APIBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBooking.Data.DataConfig
{
    public class HouseConfiguration : IEntityTypeConfiguration<EntityHouse>
    {
        public void Configure(EntityTypeBuilder<EntityHouse> builder)
        {
            builder.ToTable("House");
            builder.HasKey(k => k.id);
            builder.Property(k => k.id).ValueGeneratedOnAdd();

            builder.Property(x => x.available)
                .HasColumnType("smallint")
                .IsRequired();

           
        }
    }
}