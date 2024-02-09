﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.DAL;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArchiveProduct> ArchiveProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<VersionInfo> VersionInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=test;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArchiveProduct>(entity =>
        {
            entity.Property(e => e.Created).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Price).HasPrecision(19, 5);

            entity.HasOne(d => d.Category).WithMany(p => p.ArchiveProducts)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("FK_ArchiveProducts_Categoryid_Category_Id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Created).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Created).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Price).HasPrecision(19, 5);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("FK_Products_Categoryid_Category_Id");
        });

        modelBuilder.Entity<VersionInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VersionInfo");

            entity.HasIndex(e => e.Version, "UC_Version").IsUnique();

            entity.Property(e => e.AppliedOn).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Description).HasMaxLength(1024);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
