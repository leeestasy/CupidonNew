using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cupidon.Models;

public partial class AcquaintancesContext : DbContext
{
    public AcquaintancesContext()
    {
    }

    public AcquaintancesContext(DbContextOptions<AcquaintancesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<ZodiacSign> ZodiacSigns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-2250EVR\\SQLEXPRESS;Initial Catalog=acquaintances;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.RegistrNum).HasName("PK__Client__B11AB72735C7E494");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Login, "UQ__Client__5E55825BC092AE04").IsUnique();

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(c => c.Flag)
        .HasDefaultValue(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EducationId).HasColumnName("EducationID");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pwd)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Surname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ZodiacSignId).HasColumnName("ZodiacSignID");

            entity.HasOne(d => d.Education).WithMany(p => p.Clients)
                .HasForeignKey(d => d.EducationId)
                .HasConstraintName("FK__Client__Educatio__5629CD9C");

            entity.HasOne(d => d.Status).WithMany(p => p.Clients)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Client__StatusID__571DF1D5");

            entity.HasOne(d => d.ZodiacSign).WithMany(p => p.Clients)
                .HasForeignKey(d => d.ZodiacSignId)
                .HasConstraintName("FK__Client__Pwd__5535A963");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.EducationId).HasName("PK__Educatio__4BBE38E58D0EB824");

            entity.ToTable("Education");

            entity.Property(e => e.EducationId).HasColumnName("EducationID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF123F0531B");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Login, "UQ__Employee__5E55825BBD16E180").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pwd)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE204309EB4BE8");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZodiacSign>(entity =>
        {
            entity.HasKey(e => e.ZodiacSignId).HasName("PK__ZodiacSi__4842412291442745");

            entity.ToTable("ZodiacSign");

            entity.Property(e => e.ZodiacSignId).HasColumnName("ZodiacSignID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
