﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleFly_BookingSystem.Data;

#nullable disable

namespace SimpleFly_BookingSystem.Migrations
{
    [DbContext(typeof(SimplyFlyContext))]
    [Migration("20241113201600_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("date");

                    b.Property<int>("FlightRouteId")
                        .HasColumnType("int");

                    b.Property<int>("SeatCount")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("TotalFare")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightRouteId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.FlightRoute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<decimal>("CabinWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CheckInWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Fare")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("FlightOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightOwnerId");

                    b.ToTable("FlightRoutes");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("date");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal?>("RefundAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("RefundStatus")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.Booking", b =>
                {
                    b.HasOne("SimpleFly_BookingSystem.Models.FlightRoute", "FlightRoute")
                        .WithMany("Bookings")
                        .HasForeignKey("FlightRouteId")
                        .IsRequired()
                        .HasConstraintName("FK__Bookings__Flight__2C3393D0");

                    b.HasOne("SimpleFly_BookingSystem.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Bookings__UserId__2B3F6F97");

                    b.Navigation("FlightRoute");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.FlightRoute", b =>
                {
                    b.HasOne("SimpleFly_BookingSystem.Models.User", "FlightOwner")
                        .WithMany("FlightRoutes")
                        .HasForeignKey("FlightOwnerId")
                        .HasConstraintName("FK__FlightRou__Fligh__276EDEB3");

                    b.Navigation("FlightOwner");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.Payment", b =>
                {
                    b.HasOne("SimpleFly_BookingSystem.Models.Booking", "Booking")
                        .WithMany("Payments")
                        .HasForeignKey("BookingId")
                        .IsRequired()
                        .HasConstraintName("FK__Payments__Bookin__31EC6D26");

                    b.HasOne("SimpleFly_BookingSystem.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Payments__UserId__30F848ED");

                    b.Navigation("Booking");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.Booking", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.FlightRoute", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("SimpleFly_BookingSystem.Models.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("FlightRoutes");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
