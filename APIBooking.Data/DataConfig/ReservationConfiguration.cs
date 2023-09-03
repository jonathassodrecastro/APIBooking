using APIBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBooking.Data.DataConfig
{
    public class ReservationConfiguration : IEntityTypeConfiguration<EntityReservation>
    {
        public void Configure(EntityTypeBuilder<EntityReservation> builder)
        {
            builder.ToTable("Reservation");
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ClientId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.ClientName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.ClientLastName)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(x => x.ClientAge)
               .HasColumnType("smallint")
               .IsRequired();

            builder.Property(x => x.ClientPhoneNumber)
               .HasColumnType("varchar(30)")
               .IsRequired();

            builder.Property(x => x.StartDate)
               .HasColumnType("DATE")
               .IsRequired();

            builder.Property(x => x.EndDate)
               .HasColumnType("DATE")
               .IsRequired();

            builder.Property(x => x.HouseId)
               .HasColumnType("int")
               .IsRequired();

            builder.Property(x => x.DiscountCode)
               .HasColumnType("varchar(10)")
               .IsRequired();
        }
    }
}
