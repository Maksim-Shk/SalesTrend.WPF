using Microsoft.EntityFrameworkCore;

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
    public virtual DbSet<ProductType> ProductTypes { get; set; }

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

        //modelBuilder.Entity<Company>(entity =>
        //{
        //    entity.HasKey(e => e.CompanyId);

        //    entity.Property(e => e.Name)
        //       .HasMaxLength(255);

        //    entity.Property(e => e.Email)
        //       .HasMaxLength(255);

        //    entity.Property(e => e.Url)
        //       .HasMaxLength(255);

        //    entity.Property(e => e.ContactPersonFullName)
        //       .HasMaxLength(255);

        //    entity.HasMany<Phone>(o => o.Phones)
        //       .WithOne()
        //       .HasForeignKey(p => p.EntityId)
        //       .OnDelete(DeleteBehavior.Cascade);
        //});

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

            // Новые свойства и связи
            entity.Property(e => e.CompanyId);
            entity.Property(e => e.IndividualId);
            entity.Property(e => e.LegalEntityId);

            entity.HasOne(e => e.Company).WithMany(c => c.Addresses).HasForeignKey(e => e.CompanyId);
            entity.HasOne(e => e.Individual).WithOne(i => i.Address).HasForeignKey<Address>(e => e.IndividualId);
            entity.HasOne(e => e.LegalEntity).WithMany(le => le.Addresses).HasForeignKey(e => e.LegalEntityId);

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
                .HasForeignKey(e => e.StreetTypeId);
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

            //entity.HasMany(e => e.Addresses)
            //    .WithOne(a => a.Company)
            //    .HasForeignKey(a => a.CompanyId)
            //    .OnDelete(DeleteBehavior.Cascade);
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

            //entity.HasOne(e => e.Address)
            //    .WithOne(p => p.Individual)
            //    .HasForeignKey<Address>(e => e.CompanyId)
            //    .IsRequired();
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

            //entity.HasMany(e => e.Addresses)
            //    .WithOne(a => a.LegalEntity)
            //    .HasForeignKey(a => a.CompanyId)
            //    .OnDelete(DeleteBehavior.Cascade);
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

            entity.HasOne(e => e.ProductType)
                .WithMany(pt => pt.Products)
                .HasForeignKey(e => e.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ShortName)
                .HasMaxLength(255);

            entity.HasMany(e => e.Products)
                .WithOne(p => p.ProductType)
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<ProductType>().HasData(
            new ProductType { ProductTypeId = 1, Name = "Смартфоны", ShortName = "Смартфоны" },
            new ProductType { ProductTypeId = 2, Name = "Товары для дома", ShortName = "Товары для дома" },
            new ProductType { ProductTypeId = 3, Name = "Одежда", ShortName = "Одежда" },
            new ProductType { ProductTypeId = 4, Name = "Ноутбуки", ShortName = "Ноутбуки" },
            new ProductType { ProductTypeId = 5, Name = "Прочая электроника", ShortName = "Электроника" }
            );

        modelBuilder.Entity<Company>().HasData(
            new Company { CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "ООО ТехноТрейд 1", Email = "email1@mail.ru", Url = "https://some-site1.com", ContactPersonFullName = "Иван Иванович Иванов" },
            new Company { CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "ООО ТехноТрейд 2", Email = "email2@mail.ru", Url = "https://some-site2.com", ContactPersonFullName = "Петр Петрович Петров" },
            new Company { CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "ООО ТехноТрейд 3", Email = "email3@mail.ru", Url = "https://some-site3.com", ContactPersonFullName = "Сергей Сергеевич Сергеев" },
            new Company { CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "ООО ТехноТрейд 4", Email = "email4@mail.ru", Url = "https://some-site4.com", ContactPersonFullName = "Алексей Алексеевич Алексеев" },
            new Company { CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "ООО ТехноТрейд 5", Email = "email5@mail.ru", Url = "https://some-site5.com", ContactPersonFullName = "Дмитрий Дмитриевич Дмитриев" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, ProductTypeId = 1, Article = "1KF42", Name = "Смартфон" },
            new Product { ProductId = 2, ProductTypeId = 4, Article = "2KF42", Name = "Ноутбук" },
            new Product { ProductId = 3, ProductTypeId = 5, Article = "3KF42", Name = "Смарт-телевизор" },
            new Product { ProductId = 4, ProductTypeId = 5, Article = "4KF42", Name = "Цифровая камера" },
            new Product { ProductId = 5, ProductTypeId = 5, Article = "5KF42", Name = "Беспроводные наушники" },
            new Product { ProductId = 6, ProductTypeId = 5, Article = "6KF42", Name = "Фитнес-трекер" },
            new Product { ProductId = 7, ProductTypeId = 5, Article = "7KF42", Name = "Робот-пылесос" },
            new Product { ProductId = 8, ProductTypeId = 5, Article = "8KF42", Name = "Смарт-термостат" },
            new Product { ProductId = 9, ProductTypeId = 5, Article = "9KF42", Name = "Дрон" },
            new Product { ProductId = 10, ProductTypeId = 5, Article = "10KF42", Name = "Игровая консоль" },
            new Product { ProductId = 11, ProductTypeId = 5, Article = "11KF42", Name = "Наушники" },
            new Product { ProductId = 12, ProductTypeId = 5, Article = "12KF42", Name = "Планшет" },
            new Product { ProductId = 13, ProductTypeId = 5, Article = "13KF42", Name = "Смарт-часы" },
            new Product { ProductId = 14, ProductTypeId = 5, Article = "14KF42", Name = "Камера видеонаблюдения" },
            new Product { ProductId = 15, ProductTypeId = 5, Article = "15KF42", Name = "Электрическая зубная щетка" }
        );

        modelBuilder.Entity<PriceList>().HasData(
            new PriceList { PriceListId = 1, ReleaseDate = DateTime.UtcNow, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new PriceList { PriceListId = 2, ReleaseDate = DateTime.UtcNow.AddMonths(-1), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new PriceList { PriceListId = 3, ReleaseDate = DateTime.UtcNow.AddMonths(-2), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000001") },

            new PriceList { PriceListId = 4, ReleaseDate = DateTime.UtcNow, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
            new PriceList { PriceListId = 5, ReleaseDate = DateTime.UtcNow.AddMonths(-1), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
            new PriceList { PriceListId = 6, ReleaseDate = DateTime.UtcNow.AddMonths(-2), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000002") },

            new PriceList { PriceListId = 7, ReleaseDate = DateTime.UtcNow, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000003") },
            new PriceList { PriceListId = 8, ReleaseDate = DateTime.UtcNow.AddMonths(-1), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000003") },
            new PriceList { PriceListId = 9, ReleaseDate = DateTime.UtcNow.AddMonths(-2), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000003") },

            new PriceList { PriceListId = 10, ReleaseDate = DateTime.UtcNow, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000004") },
            new PriceList { PriceListId = 11, ReleaseDate = DateTime.UtcNow.AddMonths(-1), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000004") },
            new PriceList { PriceListId = 12, ReleaseDate = DateTime.UtcNow.AddMonths(-2), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000004") },

            new PriceList { PriceListId = 13, ReleaseDate = DateTime.UtcNow, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000005") },
            new PriceList { PriceListId = 14, ReleaseDate = DateTime.UtcNow.AddMonths(-1), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000005") },
            new PriceList { PriceListId = 15, ReleaseDate = DateTime.UtcNow.AddMonths(-2), CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000005") }
        );

        modelBuilder.Entity<PriceListProduct>().HasData(
            // Company 1
            new PriceListProduct { PriceListId = 1, ProductId = 1, Price = 100, Quantity = 5 },
            new PriceListProduct { PriceListId = 1, ProductId = 2, Price = 150, Quantity = 8 },
            new PriceListProduct { PriceListId = 1, ProductId = 3, Price = 200, Quantity = 10 },
            new PriceListProduct { PriceListId = 1, ProductId = 4, Price = 120, Quantity = 3 },
            new PriceListProduct { PriceListId = 1, ProductId = 5, Price = 80, Quantity = 15 },

            // Company 1
            new PriceListProduct { PriceListId = 2, ProductId = 1, Price = 120, Quantity = 1 },
            new PriceListProduct { PriceListId = 2, ProductId = 2, Price = 110, Quantity = 18 },
            new PriceListProduct { PriceListId = 2, ProductId = 3, Price = 250, Quantity = 11 },
            new PriceListProduct { PriceListId = 2, ProductId = 4, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 2, ProductId = 5, Price = 180, Quantity = 25 },

            // Company 1
            new PriceListProduct { PriceListId = 3, ProductId = 1, Price = 200, Quantity = 15 },
            new PriceListProduct { PriceListId = 3, ProductId = 2, Price = 130, Quantity = 5 },
            new PriceListProduct { PriceListId = 3, ProductId = 3, Price = 110, Quantity = 2 },
            new PriceListProduct { PriceListId = 3, ProductId = 4, Price = 150, Quantity = 31 },
            new PriceListProduct { PriceListId = 3, ProductId = 5, Price = 110, Quantity = 16 },

            // Company 2
            new PriceListProduct { PriceListId = 4, ProductId = 11, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 4, ProductId = 12, Price = 160, Quantity = 6 },
            new PriceListProduct { PriceListId = 4, ProductId = 13, Price = 180, Quantity = 12 },
            new PriceListProduct { PriceListId = 4, ProductId = 14, Price = 130, Quantity = 4 },
            new PriceListProduct { PriceListId = 4, ProductId = 15, Price = 90, Quantity = 20 },

            // Company 2
            new PriceListProduct { PriceListId = 5, ProductId = 11, Price = 120, Quantity = 1 },
            new PriceListProduct { PriceListId = 5, ProductId = 12, Price = 110, Quantity = 18 },
            new PriceListProduct { PriceListId = 5, ProductId = 13, Price = 250, Quantity = 11 },
            new PriceListProduct { PriceListId = 5, ProductId = 14, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 5, ProductId = 15, Price = 180, Quantity = 25 },

            // Company 2
            new PriceListProduct { PriceListId = 6, ProductId = 11, Price = 200, Quantity = 15 },
            new PriceListProduct { PriceListId = 6, ProductId = 12, Price = 130, Quantity = 5 },
            new PriceListProduct { PriceListId = 6, ProductId = 13, Price = 110, Quantity = 2 },
            new PriceListProduct { PriceListId = 6, ProductId = 14, Price = 150, Quantity = 31 },
            new PriceListProduct { PriceListId = 6, ProductId = 15, Price = 110, Quantity = 16 },

            // Company 3
            new PriceListProduct { PriceListId = 7, ProductId = 1, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 7, ProductId = 6, Price = 160, Quantity = 6 },
            new PriceListProduct { PriceListId = 7, ProductId = 7, Price = 180, Quantity = 12 },
            new PriceListProduct { PriceListId = 7, ProductId = 8, Price = 130, Quantity = 4 },
            new PriceListProduct { PriceListId = 7, ProductId = 9, Price = 90, Quantity = 20 },

            // Company 3
            new PriceListProduct { PriceListId = 8, ProductId = 1, Price = 120, Quantity = 1 },
            new PriceListProduct { PriceListId = 8, ProductId = 6, Price = 110, Quantity = 18 },
            new PriceListProduct { PriceListId = 8, ProductId = 7, Price = 250, Quantity = 11 },
            new PriceListProduct { PriceListId = 8, ProductId = 8, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 8, ProductId = 9, Price = 180, Quantity = 25 },

            // Company 3
            new PriceListProduct { PriceListId = 9, ProductId = 1, Price = 200, Quantity = 15 },
            new PriceListProduct { PriceListId = 9, ProductId = 6, Price = 130, Quantity = 5 },
            new PriceListProduct { PriceListId = 9, ProductId = 7, Price = 110, Quantity = 2 },
            new PriceListProduct { PriceListId = 9, ProductId = 8, Price = 150, Quantity = 31 },
            new PriceListProduct { PriceListId = 9, ProductId = 9, Price = 110, Quantity = 16 },

            // Company 4
            new PriceListProduct { PriceListId = 10, ProductId = 3, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 10, ProductId = 10, Price = 160, Quantity = 6 },
            new PriceListProduct { PriceListId = 10, ProductId = 11, Price = 180, Quantity = 12 },
            new PriceListProduct { PriceListId = 10, ProductId = 2, Price = 130, Quantity = 4 },
            new PriceListProduct { PriceListId = 10, ProductId = 6, Price = 90, Quantity = 20 },

            // Company 4
            new PriceListProduct { PriceListId = 11, ProductId = 3, Price = 120, Quantity = 1 },
            new PriceListProduct { PriceListId = 11, ProductId = 10, Price = 110, Quantity = 18 },
            new PriceListProduct { PriceListId = 11, ProductId = 11, Price = 250, Quantity = 11 },
            new PriceListProduct { PriceListId = 11, ProductId = 2, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 11, ProductId = 6, Price = 180, Quantity = 25 },

            // Company 4
            new PriceListProduct { PriceListId = 12, ProductId = 3, Price = 200, Quantity = 15 },
            new PriceListProduct { PriceListId = 12, ProductId = 10, Price = 130, Quantity = 5 },
            new PriceListProduct { PriceListId = 12, ProductId = 11, Price = 110, Quantity = 2 },
            new PriceListProduct { PriceListId = 12, ProductId = 2, Price = 150, Quantity = 31 },
            new PriceListProduct { PriceListId = 12, ProductId = 6, Price = 110, Quantity = 16 },
            // Company 5
            new PriceListProduct { PriceListId = 13, ProductId = 10, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 13, ProductId = 1, Price = 160, Quantity = 6 },
            new PriceListProduct { PriceListId = 13, ProductId = 14, Price = 180, Quantity = 12 },
            new PriceListProduct { PriceListId = 13, ProductId = 4, Price = 130, Quantity = 4 },
            new PriceListProduct { PriceListId = 13, ProductId = 5, Price = 90, Quantity = 20 },

            // Company 5
            new PriceListProduct { PriceListId = 14, ProductId = 10, Price = 120, Quantity = 1 },
            new PriceListProduct { PriceListId = 14, ProductId = 1, Price = 110, Quantity = 18 },
            new PriceListProduct { PriceListId = 14, ProductId = 14, Price = 250, Quantity = 11 },
            new PriceListProduct { PriceListId = 14, ProductId = 4, Price = 110, Quantity = 7 },
            new PriceListProduct { PriceListId = 14, ProductId = 5, Price = 180, Quantity = 25 },

            // Company 5
            new PriceListProduct { PriceListId = 15, ProductId = 10, Price = 200, Quantity = 15 },
            new PriceListProduct { PriceListId = 15, ProductId = 1, Price = 130, Quantity = 5 },
            new PriceListProduct { PriceListId = 15, ProductId = 14, Price = 110, Quantity = 2 },
            new PriceListProduct { PriceListId = 15, ProductId = 4, Price = 150, Quantity = 31 },
            new PriceListProduct { PriceListId = 15, ProductId = 5, Price = 110, Quantity = 16 }
        );

        modelBuilder.Entity<LegalEntity>().HasData(
            new LegalEntity { LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "ООО ТехноТрейд", TaxNumber = 123456789, ContactPersonFullName = "Иванов Иван Иванович", Email = "ivanov@example.com" },
            new LegalEntity { LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "ЗАО ЭлектроМагазин", TaxNumber = 987654321, ContactPersonFullName = "Петров Петр Петрович", Email = "petrov@example.com" },
            new LegalEntity { LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "ПАО Инновационные Решения", TaxNumber = 567890123, ContactPersonFullName = "Сидоров Сидор Сидорович", Email = "sidorov@example.com" },
            new LegalEntity { LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "ООО Торговый Элит", TaxNumber = 234567890, ContactPersonFullName = "Александров Александр Александрович", Email = "alexandrov@example.com" },
            new LegalEntity { LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "ЗАО Продвинутые Технологии", TaxNumber = 678901234, ContactPersonFullName = "Дмитриев Дмитрий Дмитриевич", Email = "dmitriev@example.com" }
        );

        modelBuilder.Entity<Individual>().HasData(
            new Individual { IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Иван", Surname = "Иванов", Patronymic = "Иванович", TaxNumber = 123456789, PassportSerial = 1234, PassportNumber = 567890, IssueDate = DateTime.UtcNow, IssuedBy = "УФМС" },
            new Individual { IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Петр", Surname = "Петров", Patronymic = "Петрович", TaxNumber = 987654321, PassportSerial = 5678, PassportNumber = 123456, IssueDate = DateTime.UtcNow, IssuedBy = "УФМС" },
            new Individual { IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Сидор", Surname = "Сидоров", Patronymic = "Сидорович", TaxNumber = 567890123, PassportSerial = 9101, PassportNumber = 112233, IssueDate = DateTime.UtcNow, IssuedBy = "УФМС" },
            new Individual { IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Александр", Surname = "Александров", Patronymic = "Александрович", TaxNumber = 234567890, PassportSerial = 1415, PassportNumber = 161718, IssueDate = DateTime.UtcNow, IssuedBy = "УФМС" },
            new Individual { IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Дмитрий", Surname = "Дмитриев", Patronymic = "Дмитриевич", TaxNumber = 678901234, PassportSerial = 1920, PassportNumber = 212223, IssueDate = DateTime.UtcNow, IssuedBy = "УФМС" }
        );



        modelBuilder.Entity<Locality>().HasData(
    new Locality { LocalityId = 1, LocalityTypeId = 1, Name = "Оренбург", ShortName = "Орен." },
    new Locality { LocalityId = 2, LocalityTypeId = 2, Name = "Вязовка", ShortName = "Вязовка" },
    new Locality { LocalityId = 3, LocalityTypeId = 3, Name = "Березовка", ShortName = "Березовк." },
    new Locality { LocalityId = 4, LocalityTypeId = 4, Name = "Заборье", ShortName = "Заборье" },
    new Locality { LocalityId = 5, LocalityTypeId = 5, Name = "Черничка", ShortName = "Чернич." }
);

        modelBuilder.Entity<LocalityType>().HasData(
            new LocalityType { LocalityTypeId = 1, Name = "Город", ShortName = "г." },
            new LocalityType { LocalityTypeId = 2, Name = "Поселок", ShortName = "п." },
            new LocalityType { LocalityTypeId = 3, Name = "Деревня", ShortName = "д." },
            new LocalityType { LocalityTypeId = 4, Name = "Хутор", ShortName = "х." },
            new LocalityType { LocalityTypeId = 5, Name = "Село", ShortName = "с." }
        );

        modelBuilder.Entity<StreetType>().HasData(
            new StreetType { StreetTypeId = 1, Name = "Улица", ShortName = "ул." },
            new StreetType { StreetTypeId = 2, Name = "Проспект", ShortName = "пр." },
            new StreetType { StreetTypeId = 3, Name = "Бульвар", ShortName = "бул." },
            new StreetType { StreetTypeId = 4, Name = "Переулок", ShortName = "пер." },
            new StreetType { StreetTypeId = 5, Name = "Проезд", ShortName = "пр-д" }
        );

        modelBuilder.Entity<Street>().HasData(
            new Street { StreetId = 1, StreetTypeId = 1, Name = "Новая", ShortName = "ул." },
            new Street { StreetId = 2, StreetTypeId = 2, Name = "Мира", ShortName = "пр." },
            new Street { StreetId = 3, StreetTypeId = 3, Name = "Центральный", ShortName = "бул." },
            new Street { StreetId = 4, StreetTypeId = 4, Name = "Молодежный", ShortName = "пер." },
            new Street { StreetId = 5, StreetTypeId = 5, Name = "Луговой", ShortName = "пр-д" }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address { AddressId = 1, House = "1241", Apartment = "114", LocalityId = 1, StreetId = 1, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Address { AddressId = 2, House = "1242", Apartment = "124", LocalityId = 1, StreetId = 1, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
            new Address { AddressId = 3, House = "1234", Apartment = "141", LocalityId = 1, StreetId = 1, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000003") },
            new Address { AddressId = 4, House = "1424", Apartment = "134", LocalityId = 2, StreetId = 2, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000004") },
            new Address { AddressId = 5, House = "1524", Apartment = "141", LocalityId = 2, StreetId = 2, CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000005") },
            new Address { AddressId = 6, House = "1624", Apartment = "144", LocalityId = 2, StreetId = 2, LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Address { AddressId = 7, House = "1274", Apartment = "14", LocalityId = 3, StreetId = 3, LegalEntityId =  Guid.Parse("00000000-0000-0000-0000-000000000002") },
            new Address { AddressId = 8, House = "1124", Apartment = "514", LocalityId = 3, StreetId = 3, LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000003") },
            new Address { AddressId = 9, House = "1242", Apartment = "124", LocalityId = 3, StreetId = 3, LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000004") },
            new Address { AddressId = 10, House = "124", Apartment = "214", LocalityId = 4, StreetId = 4, LegalEntityId = Guid.Parse("00000000-0000-0000-0000-000000000005") },
            new Address { AddressId = 11, House = "124", Apartment = "714", LocalityId = 4, StreetId = 4, IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Address { AddressId = 12, House = "124", Apartment = "814", LocalityId = 4, StreetId = 4, IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
            new Address { AddressId = 13, House = "444", Apartment = "14", LocalityId = 5, StreetId = 5, IndividualId =  Guid.Parse("00000000-0000-0000-0000-000000000003") },
            new Address { AddressId = 14, House = "111", Apartment = "194", LocalityId = 5, StreetId = 5, IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000004") },
            new Address { AddressId = 15, House = "222", Apartment = "149", LocalityId = 5, StreetId = 5, IndividualId = Guid.Parse("00000000-0000-0000-0000-000000000005") }
            );

        modelBuilder.Entity<ClientOrder>().HasData(
            new ClientOrder { ClientOrderId = 1, ClientId = Guid.Parse("00000000-0000-0000-0000-000000000001"), OrderDate = DateTime.UtcNow },
            new ClientOrder { ClientOrderId = 2, ClientId = Guid.Parse("00000000-0000-0000-0000-000000000002"), OrderDate = DateTime.UtcNow },
            new ClientOrder { ClientOrderId = 3, ClientId = Guid.Parse("00000000-0000-0000-0000-000000000003"), OrderDate = DateTime.UtcNow },
            new ClientOrder { ClientOrderId = 4, ClientId = Guid.Parse("00000000-0000-0000-0000-000000000004"), OrderDate = DateTime.UtcNow },
            new ClientOrder { ClientOrderId = 5, ClientId = Guid.Parse("00000000-0000-0000-0000-000000000005"), OrderDate = DateTime.UtcNow }
            );

        modelBuilder.Entity<ClientOrderProduct>().HasData(
            new ClientOrderProduct { ClientOrderProductId = 1, ClientOrderId = 1, ProductId = 1, Quantity = 5 },
            new ClientOrderProduct { ClientOrderProductId = 2, ClientOrderId = 1, ProductId = 2, Quantity = 2 },
            new ClientOrderProduct { ClientOrderProductId = 3, ClientOrderId = 1, ProductId = 3, Quantity = 6 },
            new ClientOrderProduct { ClientOrderProductId = 4, ClientOrderId = 1, ProductId = 4, Quantity = 1 },

            new ClientOrderProduct { ClientOrderProductId = 5, ClientOrderId = 2, ProductId = 5, Quantity = 2 },
            new ClientOrderProduct { ClientOrderProductId = 6, ClientOrderId = 2, ProductId = 6, Quantity = 5 },
            new ClientOrderProduct { ClientOrderProductId = 7, ClientOrderId = 2, ProductId = 7, Quantity = 8 },
            new ClientOrderProduct { ClientOrderProductId = 8, ClientOrderId = 2, ProductId = 8, Quantity = 1 },

            new ClientOrderProduct { ClientOrderProductId = 9, ClientOrderId = 3, ProductId = 1, Quantity = 2 },
            new ClientOrderProduct { ClientOrderProductId = 10, ClientOrderId = 3, ProductId = 15, Quantity = 5 },
            new ClientOrderProduct { ClientOrderProductId = 11, ClientOrderId = 3, ProductId = 14, Quantity = 8 },
            new ClientOrderProduct { ClientOrderProductId = 12, ClientOrderId = 3, ProductId = 13, Quantity = 1 },

            new ClientOrderProduct { ClientOrderProductId = 13, ClientOrderId = 4, ProductId = 12, Quantity = 2 },
            new ClientOrderProduct { ClientOrderProductId = 14, ClientOrderId = 4, ProductId = 11, Quantity = 5 },
            new ClientOrderProduct { ClientOrderProductId = 15, ClientOrderId = 4, ProductId = 10, Quantity = 8 },
            new ClientOrderProduct { ClientOrderProductId = 16, ClientOrderId = 4, ProductId = 8, Quantity = 1 },

            new ClientOrderProduct { ClientOrderProductId = 17, ClientOrderId = 5, ProductId = 9, Quantity = 2 },
            new ClientOrderProduct { ClientOrderProductId = 18, ClientOrderId = 5, ProductId = 1, Quantity = 5 },
            new ClientOrderProduct { ClientOrderProductId = 19, ClientOrderId = 5, ProductId = 2, Quantity = 8 },
            new ClientOrderProduct { ClientOrderProductId = 20, ClientOrderId = 5, ProductId = 5, Quantity = 1 }
            );

        modelBuilder.Entity<Phone>().HasData(
            new Phone { PhoneId = 1, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Phone { PhoneId = 2, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
            new Phone { PhoneId = 3, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000003") },
            new Phone { PhoneId = 4, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000004") },
            new Phone { PhoneId = 5, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000005") },
            new Phone { PhoneId = 6, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Phone { PhoneId = 7, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
            new Phone { PhoneId = 8, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000003") },
            new Phone { PhoneId = 9, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000004") },
            new Phone { PhoneId = 10, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000005") },
            new Phone { PhoneId = 11, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Phone { PhoneId = 12, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
            new Phone { PhoneId = 13, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000003") },
            new Phone { PhoneId = 14, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000004") },
            new Phone { PhoneId = 15, PhoneNumber = "+78005553535", EntityId = Guid.Parse("00000000-0000-0000-0000-000000000005") }
            );
    }
}
