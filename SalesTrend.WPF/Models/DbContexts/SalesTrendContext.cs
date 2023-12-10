using SalesTrend.WPF.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Accord;

namespace SalesTrend.WPF.Models;

public class SalesTrendContext : DbContext
{
    public SalesTrendContext() { }

    public SalesTrendContext(DbContextOptions<SalesTrendContext> options)
    : base(options) { }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Locality> Localities { get; set; }
    public virtual DbSet<LocalityType> LocalityTypes { get; set; }
    public virtual DbSet<Phone> Phones { get; set; }
    public virtual DbSet<Street> Streets { get; set; }
    public virtual DbSet<StreetType> StreetTypes { get; set; }
    public virtual DbSet<ClientOrder> ClientOrders { get; set; }
    public virtual DbSet<ClientOrderProduct> ClientOrderProducts { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Individual> Individuals { get; set; }
    public virtual DbSet<LegalEntity> LegalEntities { get; set; }
    public virtual DbSet<PriceList> PriceLists { get; set; }
    public virtual DbSet<PriceListProduct> PriceListProducts { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5434;Database=SalesTrend;Username=postgres;Password=password");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SalesTrend;Username=postgres;Password=password");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.SetTableName(entityType.DisplayName());
        }

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId);

            entity.Property(e => e.Name)
               .HasMaxLength(255);

            entity.Property(e => e.Email)
               .HasMaxLength(255);

            entity.Property(e => e.Url)
               .HasMaxLength(255);

            entity.Property(e => e.ContactPersonFullName)
               .HasMaxLength(255);

            entity.HasMany<Phone>(o => o.Phones)
               .WithOne()
               .HasForeignKey(p => p.EntityId)
               .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId);

            entity.Property(e => e.House)
                .HasMaxLength(255);

            entity.Property(e => e.Apartment)
                .HasMaxLength(255);

            entity.HasOne(e => e.Locality)
                .WithMany(l => l.Addresses)
                .HasForeignKey(e => e.LocalityId);

            entity.HasOne(e => e.Street)
                .WithMany(s => s.Addresses)
                .HasForeignKey(e => e.StreetId);
        });

        modelBuilder.Entity<Locality>(entity =>
        {
            entity.HasKey(e => e.LocalityId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ShortName)
                .HasMaxLength(255);

            entity.HasOne(e => e.LocalityType)
                .WithMany(t => t.Localities)
                .HasForeignKey(e => e.LocalityTypeId);
        });

        modelBuilder.Entity<LocalityType>(entity =>
        {
            entity.HasKey(e => e.LocalityTypeId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ShortName)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.PhoneId);

            entity.Property(e => e.PhoneNumber)
                .IsRequired();

            // Общее свойство для хранения CompanyId или LegalEntityId
            entity.Property(e => e.EntityId)
                .IsRequired();

            entity.HasOne(e => e.Company)
                .WithMany(c => c.Phones)
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.LegalEntity)
                .WithMany(le => le.Phones)
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.HasKey(e => e.StreetId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ShortName)
                .HasMaxLength(255);

            entity.HasOne(e => e.StreetType)
                .WithMany()
                .HasForeignKey(e => e.StreeTypetId);
        });

        modelBuilder.Entity<StreetType>(entity =>
        {
            entity.HasKey(e => e.StreetTypeId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ShortName)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<ClientOrder>(entity =>
        {
            entity.HasKey(e => e.ClientOrderId);

            entity.HasOne(e => e.Individual)
                .WithMany()
                .HasForeignKey(e => e.ClientId);

            entity.HasOne(e => e.LegalEntity)
                .WithMany()
                .HasForeignKey(e => e.ClientId);
        });

        modelBuilder.Entity<ClientOrderProduct>(entity =>
        {
            entity.HasKey(e => e.ClientOrderProductId);

            entity.HasOne(e => e.Product)
                .WithMany(p => p.ClientOrderProducts)
                .HasForeignKey(e => e.ProductId);

            entity.HasOne(e => e.ClientOrder)
                .WithMany(co => co.ClientOrderProducts)
                .HasForeignKey(e => e.ClientOrderId);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Email)
                .HasMaxLength(255);

            entity.Property(e => e.Url)
                .HasMaxLength(255);

            entity.Property(e => e.ContactPersonFullName)
                .HasMaxLength(255);

            entity.HasMany(e => e.Phones)
                .WithOne(c => c.Company)
                .HasForeignKey(p => p.EntityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Addresses)
                .WithOne(a => a.Company)
                .HasForeignKey(a => a.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Individual>(entity =>
        {
            entity.HasKey(e => e.IndividualId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Patronymic)
                .HasMaxLength(255);

            entity.HasOne(e => e.Address)
                .WithOne(p => p.Individual)
                .HasForeignKey<Individual>(e => e.AddressId)
                .IsRequired();
        });


        modelBuilder.Entity<LegalEntity>(entity =>
        {
            entity.HasKey(e => e.LegalEntityId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ShortName)
                .HasMaxLength(255);

            entity.Property(e => e.ContactPersonFullName)
                .HasMaxLength(255);

            entity.Property(e => e.Email)
                .HasMaxLength(255);

            entity.HasMany(e => e.Phones)
                .WithOne(p => p.LegalEntity)
                .HasForeignKey(p => p.EntityId)  // Используем одинаковый внешний ключ
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Addresses)
                .WithOne(a => a.LegalEntity)
                .HasForeignKey(a => a.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PriceList>(entity =>
        {
            entity.HasKey(e => e.PriceListId);

            entity.HasMany(e => e.PriceListProducts)
                .WithOne(p => p.PriceList)
                .HasForeignKey(p => p.PriceListId);
        });

        modelBuilder.Entity<PriceListProduct>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.PriceListId });

            entity.HasOne(e => e.Product)
                .WithMany(p => p.PriceListProducts)
                .HasForeignKey(e => e.ProductId);

            entity.HasOne(e => e.PriceList)
                .WithMany(pl => pl.PriceListProducts)
                .HasForeignKey(e => e.PriceListId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.Article)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        });
    }
}
