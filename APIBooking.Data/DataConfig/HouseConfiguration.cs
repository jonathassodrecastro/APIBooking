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
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Available)
                .HasColumnType("smallint")
                .IsRequired();


        }
    }
}