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
    public class ReservationConfiguration : IEntityTypeConfiguration<EntityReservation>
    {
        public void Configure(EntityTypeBuilder<EntityReservation> builder)
        {
            builder.ToTable("Reservation");
            builder.HasKey(k => k.id);
            builder.Property(k => k.id).ValueGeneratedOnAdd();

            builder.Property(x => x.clientId)
                .HasColumnType("smallint")
                .IsRequired();

            builder.Property(x => x.clientName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.clientLastname)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(x => x.clientAge)
               .HasColumnType("smallint")
               .IsRequired();

            builder.Property(x => x.clientPhoneNumber)
               .HasColumnType("varchar(30)")
               .IsRequired();

            builder.Property(x => x.startDate)
               .HasColumnType("date")
               .IsRequired();

            builder.Property(x => x.endDate)
               .HasColumnType("date")
               .IsRequired();

            builder.Property(x => x.houseId)
               .HasColumnType("smallint")
               .IsRequired();

            builder.Property(x => x.discountCode)
               .HasColumnType("varchar(10)")
               .IsRequired();
        }
    }
}
