using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace S6FirstEntityFramework.api.Models;

public partial class PeruContext : DbContext
{
    public PeruContext()
    {
    }

    public PeruContext(DbContextOptions<PeruContext> options)
        : base(options)
    {
    }

    public virtual DbSet<People> People { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= DESKTOP-V818EEU\\SQLEXPRESS;Database=PERU; Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<People>(entity =>
        {
            entity.ToTable("people");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.IsActive).HasColumnName("isActive");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
