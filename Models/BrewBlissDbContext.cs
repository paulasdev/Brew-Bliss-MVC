using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BrewBlissApp.Models;

public partial class BrewBlissDbContext : DbContext
{
    public BrewBlissDbContext()
    {
    }

    public BrewBlissDbContext(DbContextOptions<BrewBlissDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ContactMessage> ContactMessages { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=BrewBlissDB;User Id=sa;Password=Carol@2006;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B6238C539");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContactM__3214EC07BF5B2AC6");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.MenuItemId).HasName("PK__MenuItem__8943F702F7844F76");

            entity.ToTable("MenuItem");

            entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_MenuItem_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
