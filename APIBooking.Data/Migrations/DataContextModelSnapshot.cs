using APIBooking.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace APIBooking.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("APIBooking.Domain.Entities.EntityClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<short>("Age")
                        .HasColumnType("smallint")
                        .HasAnnotation("Relational:JsonPropertyName", "Age");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "LastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "Name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasAnnotation("Relational:JsonPropertyName", "PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("APIBooking.Domain.Entities.EntityHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<short>("Available")
                        .HasColumnType("smallint")
                        .HasAnnotation("Relational:JsonPropertyName", "available");

                    b.HasKey("Id");

                    b.ToTable("House", (string)null);
                });

            modelBuilder.Entity("APIBooking.Domain.Entities.EntityReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "Reservation_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<short>("ClientAge")
                        .HasColumnType("smallint")
                        .HasAnnotation("Relational:JsonPropertyName", "ClientAge");

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "ClientId");

                    b.Property<string>("ClientLastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "ClientLastname");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("Relational:JsonPropertyName", "ClientName");

                    b.Property<string>("ClientPhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasAnnotation("Relational:JsonPropertyName", "ClientPhoneNumber");

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasAnnotation("Relational:JsonPropertyName", "DiscountCode");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("DATE")
                        .HasAnnotation("Relational:JsonPropertyName", "EndDate");

                    b.Property<int>("HouseId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "HouseId");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("DATE")
                        .HasAnnotation("Relational:JsonPropertyName", "StartDate");

                    b.HasKey("Id");

                    b.ToTable("Reservation", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
