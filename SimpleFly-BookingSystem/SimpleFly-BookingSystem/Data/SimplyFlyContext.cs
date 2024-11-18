using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Data
{
    public partial class SimplyFlyContext : DbContext
    {
        public SimplyFlyContext()
        {
        }

        public SimplyFlyContext(DbContextOptions<SimplyFlyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<FlightRoute> FlightRoutes { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=SimpleFlyDB; User Id=sa; Password=Admin@123; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.FlightRoute)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.FlightRouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bookings__Flight__2C3393D0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bookings__UserId__2B3F6F97");
            });

            modelBuilder.Entity<FlightRoute>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Destination).HasMaxLength(100);

                entity.Property(e => e.Origin).HasMaxLength(100);

                entity.HasOne(d => d.FlightOwner)
                    .WithMany(p => p.FlightRoutes)
                    .HasForeignKey(d => d.FlightOwnerId)
                    .HasConstraintName("FK__FlightRou__Fligh__276EDEB3");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.Property(e => e.PaymentStatus).HasMaxLength(20);

                entity.Property(e => e.RefundAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.RefundStatus).HasMaxLength(20);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments__Bookin__31EC6D26");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments__UserId__30F848ED");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.PasswordHash).HasMaxLength(256);

                entity.Property(e => e.Role).HasMaxLength(20);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
