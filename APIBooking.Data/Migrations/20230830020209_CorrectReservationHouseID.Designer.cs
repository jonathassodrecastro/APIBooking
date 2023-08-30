﻿// <auto-generated />
using System;
using APIBooking.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APIBooking.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230830020209_CorrectReservationHouseID")]
    partial class CorrectReservationHouseID
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("APIBooking.Domain.Entities.EntityClient", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<short>("age")
                        .HasColumnType("smallint");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("APIBooking.Domain.Entities.EntityHouse", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<short>("available")
                        .HasColumnType("smallint");

                    b.HasKey("id");

                    b.ToTable("House", (string)null);
                });

            modelBuilder.Entity("APIBooking.Domain.Entities.EntityReservation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<short>("clientAge")
                        .HasColumnType("smallint");

                    b.Property<short>("clientId")
                        .HasColumnType("smallint");

                    b.Property<string>("clientLastname")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("clientName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("clientPhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("discountCode")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("DATE");

                    b.Property<int>("houseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("DATE");

                    b.HasKey("id");

                    b.ToTable("Reservation", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
