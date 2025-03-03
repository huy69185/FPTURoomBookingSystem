using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClassroomBooking.Repository.Entities;

public partial class ClassroomBookingDbContext : DbContext
{
    public ClassroomBookingDbContext()
    {
    }

    public ClassroomBookingDbContext(DbContextOptions<ClassroomBookingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Classrooms> Classrooms { get; set; }

    public virtual DbSet<Students> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NGUYEN\\NGUYEN;Uid=sa;Pwd=12345;Database= ClassroomBookingDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED38404CA8");

            entity.Property(e => e.BookingStatus).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Purpose).HasMaxLength(200);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.StudentCode).HasMaxLength(20);

            entity.HasOne(d => d.Classroom).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Classrooms");

            entity.HasOne(d => d.StudentCodeNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StudentCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Students");
        });

        modelBuilder.Entity<Classrooms>(entity =>
        {
            entity.HasKey(e => e.ClassroomId).HasName("PK__Classroo__11618EAA04AA2C4F");

            entity.Property(e => e.BuildingName).HasMaxLength(50);
            entity.Property(e => e.ClassroomName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Students>(entity =>
        {
            entity.HasKey(e => e.StudentCode).HasName("PK__Students__1FC8860549C5A5AF");

            entity.Property(e => e.StudentCode).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Campus).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
