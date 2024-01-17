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
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=SalesTrend;Username=app;Password=password");
            }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.SetTableName(entityType.DisplayName());
        }

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
            // Новые свойства и связи

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
                .HasForeignKey(p => p.EntityId) 
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
            new Product { ProductId = 15, ProductTypeId = 5, Article = "15KF42", Name = "Электрическая зубная щетка" },


            new Product { ProductId = 21, ProductTypeId = 1, Article = "1KF42", Name = "Смартфон" },
            new Product { ProductId = 22, ProductTypeId = 4, Article = "2KF42", Name = "Ноутбук" },
            new Product { ProductId = 23, ProductTypeId = 5, Article = "3KF42", Name = "Планшет" },
            new Product { ProductId = 24, ProductTypeId = 5, Article = "4KF42", Name = "Телевизор" },
            new Product { ProductId = 25, ProductTypeId = 5, Article = "5KF42", Name = "Наушники" },
            new Product { ProductId = 26, ProductTypeId = 5, Article = "6KF42", Name = "Камера" },
            new Product { ProductId = 27, ProductTypeId = 5, Article = "7KF42", Name = "Фотоаппарат" },
            new Product { ProductId = 28, ProductTypeId = 5, Article = "8KF42", Name = "Монитор" },
            new Product { ProductId = 29, ProductTypeId = 5, Article = "9KF42", Name = "Принтер" },
            new Product { ProductId = 210, ProductTypeId = 5, Article = "10KF42", Name = "Роутер" },
            new Product { ProductId = 211, ProductTypeId = 5, Article = "11KF42", Name = "Смарт-часы" },
            new Product { ProductId = 212, ProductTypeId = 5, Article = "12KF42", Name = "Электронная книга" },
            new Product { ProductId = 213, ProductTypeId = 5, Article = "13KF42", Name = "Умный домофон" },
            new Product { ProductId = 214, ProductTypeId = 5, Article = "14KF42", Name = "Аккумулятор" },
            new Product { ProductId = 215, ProductTypeId = 5, Article = "15KF42", Name = "Портативная зарядка" },
            new Product { ProductId = 216, ProductTypeId = 5, Article = "16KF42", Name = "Гарнитура" },
            new Product { ProductId = 217, ProductTypeId = 5, Article = "17KF42", Name = "Bluetooth-динамик" },
            new Product { ProductId = 218, ProductTypeId = 5, Article = "18KF42", Name = "Веб-камера" },
            new Product { ProductId = 219, ProductTypeId = 5, Article = "19KF42", Name = "Флеш-накопитель" },
            new Product { ProductId = 220, ProductTypeId = 5, Article = "20KF42", Name = "SSD-накопитель" },
            new Product { ProductId = 221, ProductTypeId = 5, Article = "21KF42", Name = "Внешний жесткий диск" },
            new Product { ProductId = 222, ProductTypeId = 5, Article = "22KF42", Name = "Игровая консоль" },
            new Product { ProductId = 223, ProductTypeId = 5, Article = "23KF42", Name = "VR-шлем" },
            new Product { ProductId = 224, ProductTypeId = 5, Article = "24KF42", Name = "Смарт-телевизор" },
            new Product { ProductId = 225, ProductTypeId = 5, Article = "25KF42", Name = "Микрофон" },
            new Product { ProductId = 226, ProductTypeId = 5, Article = "26KF42", Name = "Тонер-картридж" },
            new Product { ProductId = 227, ProductTypeId = 5, Article = "27KF42", Name = "Манипулятор (мышь, трекбол)" },
            new Product { ProductId = 228, ProductTypeId = 5, Article = "28KF42", Name = "Адаптер Wi-Fi" },
            new Product { ProductId = 229, ProductTypeId = 5, Article = "29KF42", Name = "Видеокарта" },
            new Product { ProductId = 230, ProductTypeId = 5, Article = "30KF42", Name = "Процессор" },
            new Product { ProductId = 231, ProductTypeId = 5, Article = "31KF42", Name = "Материнская плата" },
            new Product { ProductId = 232, ProductTypeId = 5, Article = "32KF42", Name = "Оперативная память (RAM)" },
            new Product { ProductId = 233, ProductTypeId = 5, Article = "33KF42", Name = "Компьютерная мышь" },
            new Product { ProductId = 234, ProductTypeId = 5, Article = "34KF42", Name = "Клавиатура" },
            new Product { ProductId = 235, ProductTypeId = 5, Article = "35KF42", Name = "USB-хаб" },
            new Product { ProductId = 236, ProductTypeId = 5, Article = "36KF42", Name = "HDMI-кабель" },
            new Product { ProductId = 237, ProductTypeId = 5, Article = "37KF42", Name = "Сетевой фильтр" },
            new Product { ProductId = 238, ProductTypeId = 5, Article = "38KF42", Name = "Электронный браслет" },
            new Product { ProductId = 239, ProductTypeId = 5, Article = "39KF42", Name = "Внешний оптический привод" },
            new Product { ProductId = 240, ProductTypeId = 5, Article = "40KF42", Name = "Игровой контроллер" },
            new Product { ProductId = 241, ProductTypeId = 5, Article = "41KF42", Name = "Стабилизатор напряжения" },
            new Product { ProductId = 242, ProductTypeId = 5, Article = "42KF42", Name = "Термопаста" },
            new Product { ProductId = 243, ProductTypeId = 5, Article = "43KF42", Name = "Солнечная батарея" },
            new Product { ProductId = 244, ProductTypeId = 5, Article = "44KF42", Name = "GPS-навигатор" }
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
            new PriceListProduct { PriceListId = 1, ProductId = 1, Price = 10000, Quantity = 5 },
            new PriceListProduct { PriceListId = 1, ProductId = 2, Price = 15000, Quantity = 8 },
            new PriceListProduct { PriceListId = 1, ProductId = 3, Price = 20000, Quantity = 10 },
            new PriceListProduct { PriceListId = 1, ProductId = 4, Price = 12000, Quantity = 3 },
            new PriceListProduct { PriceListId = 1, ProductId = 5, Price = 8000, Quantity = 15 },

            // Company 1
            new PriceListProduct { PriceListId = 2, ProductId = 1, Price = 12000, Quantity = 1 },
            new PriceListProduct { PriceListId = 2, ProductId = 2, Price = 11000, Quantity = 18 },
            new PriceListProduct { PriceListId = 2, ProductId = 3, Price = 33000, Quantity = 11 },
            new PriceListProduct { PriceListId = 2, ProductId = 4, Price = 11000, Quantity = 7 },
            new PriceListProduct { PriceListId = 2, ProductId = 5, Price = 32000, Quantity = 25 },

            // Company 1
            new PriceListProduct { PriceListId = 3, ProductId = 1, Price = 20000, Quantity = 15 },
            new PriceListProduct { PriceListId = 3, ProductId = 2, Price = 13000, Quantity = 5 },
            new PriceListProduct { PriceListId = 3, ProductId = 3, Price = 11000, Quantity = 2 },
            new PriceListProduct { PriceListId = 3, ProductId = 4, Price = 15000, Quantity = 31 },
            new PriceListProduct { PriceListId = 3, ProductId = 5, Price = 11000, Quantity = 16 },

            // Company 2
            new PriceListProduct { PriceListId = 4, ProductId = 11, Price = 11000, Quantity = 7 },
            new PriceListProduct { PriceListId = 4, ProductId = 12, Price = 16000, Quantity = 6 },
            new PriceListProduct { PriceListId = 4, ProductId = 13, Price = 32000, Quantity = 12 },
            new PriceListProduct { PriceListId = 4, ProductId = 14, Price = 13000, Quantity = 4 },
            new PriceListProduct { PriceListId = 4, ProductId = 15, Price = 9000, Quantity = 20 },

            // Company 2
            new PriceListProduct { PriceListId = 5, ProductId = 11, Price = 12000, Quantity = 1 },
            new PriceListProduct { PriceListId = 5, ProductId = 12, Price = 11000, Quantity = 18 },
            new PriceListProduct { PriceListId = 5, ProductId = 13, Price = 33000, Quantity = 11 },
            new PriceListProduct { PriceListId = 5, ProductId = 14, Price = 11000, Quantity = 7 },
            new PriceListProduct { PriceListId = 5, ProductId = 15, Price = 18000, Quantity = 25 },

            // Company 2
            new PriceListProduct { PriceListId = 6, ProductId = 11, Price = 20000, Quantity = 15 },
            new PriceListProduct { PriceListId = 6, ProductId = 12, Price = 13000, Quantity = 5 },
            new PriceListProduct { PriceListId = 6, ProductId = 13, Price = 11000, Quantity = 2 },
            new PriceListProduct { PriceListId = 6, ProductId = 14, Price = 15000, Quantity = 31 },
            new PriceListProduct { PriceListId = 6, ProductId = 15, Price = 11000, Quantity = 16 },

            // Company 3
            new PriceListProduct { PriceListId = 7, ProductId = 1, Price = 11000, Quantity = 7 },
            new PriceListProduct { PriceListId = 7, ProductId = 6, Price = 16000, Quantity = 6 },
            new PriceListProduct { PriceListId = 7, ProductId = 7, Price = 18000, Quantity = 12 },
            new PriceListProduct { PriceListId = 7, ProductId = 8, Price = 13000, Quantity = 4 },
            new PriceListProduct { PriceListId = 7, ProductId = 9, Price = 9000, Quantity = 20 },

            // Company 3
            new PriceListProduct { PriceListId = 8, ProductId = 1, Price = 12000, Quantity = 1 },
            new PriceListProduct { PriceListId = 8, ProductId = 6, Price = 11000, Quantity = 18 },
            new PriceListProduct { PriceListId = 8, ProductId = 7, Price = 34000, Quantity = 11 },
            new PriceListProduct { PriceListId = 8, ProductId = 8, Price = 11000, Quantity = 7 },
            new PriceListProduct { PriceListId = 8, ProductId = 9, Price = 18000, Quantity = 25 },

            // Company 3
            new PriceListProduct { PriceListId = 9, ProductId = 1, Price = 20000, Quantity = 15 },
            new PriceListProduct { PriceListId = 9, ProductId = 6, Price = 13000, Quantity = 5 },
            new PriceListProduct { PriceListId = 9, ProductId = 7, Price = 11000, Quantity = 2 },
            new PriceListProduct { PriceListId = 9, ProductId = 8, Price = 15000, Quantity = 31 },
            new PriceListProduct { PriceListId = 9, ProductId = 9, Price = 11000, Quantity = 16 },

            // Company 4
            new PriceListProduct { PriceListId = 10, ProductId = 3, Price = 23000, Quantity = 7 },
            new PriceListProduct { PriceListId = 10, ProductId = 10, Price =46000, Quantity = 6 },
            new PriceListProduct { PriceListId = 10, ProductId = 11, Price =60000, Quantity = 12 },
            new PriceListProduct { PriceListId = 10, ProductId = 2, Price = 46000, Quantity = 4 },
            new PriceListProduct { PriceListId = 10, ProductId = 6, Price = 23000, Quantity = 20 },

            // Company 4
            new PriceListProduct { PriceListId = 11, ProductId = 3, Price = 46000, Quantity = 1 },
            new PriceListProduct { PriceListId = 11, ProductId = 10, Price =53000, Quantity = 18 },
            new PriceListProduct { PriceListId = 11, ProductId = 11, Price =62000, Quantity = 11 },
            new PriceListProduct { PriceListId = 11, ProductId = 2, Price = 55000, Quantity = 7 },
            new PriceListProduct { PriceListId = 11, ProductId = 6, Price = 35000, Quantity = 25 },

            // Company 4
            new PriceListProduct { PriceListId = 12, ProductId = 3, Price = 15000, Quantity = 15 },
            new PriceListProduct { PriceListId = 12, ProductId = 10, Price =14300, Quantity = 5 },
            new PriceListProduct { PriceListId = 12, ProductId = 11, Price =420000, Quantity = 2 },
            new PriceListProduct { PriceListId = 12, ProductId = 2, Price = 33000, Quantity = 31 },
            new PriceListProduct { PriceListId = 12, ProductId = 6, Price = 29000, Quantity = 16 },
            // Company 5
            new PriceListProduct { PriceListId = 13, ProductId = 10, Price =17000 , Quantity = 7 },
            new PriceListProduct { PriceListId = 13, ProductId = 1, Price = 26000 , Quantity = 6 },
            new PriceListProduct { PriceListId = 13, ProductId = 14, Price =31000 , Quantity = 12 },
            new PriceListProduct { PriceListId = 13, ProductId = 4, Price = 20000 , Quantity = 4 },
            new PriceListProduct { PriceListId = 13, ProductId = 5, Price = 18000 , Quantity = 20 },

            // Company 5
            new PriceListProduct { PriceListId = 14, ProductId = 10, Price =55000 , Quantity = 1 },
            new PriceListProduct { PriceListId = 14, ProductId = 1, Price = 42000 , Quantity = 18 },
            new PriceListProduct { PriceListId = 14, ProductId = 14, Price =51000 , Quantity = 11 },
            new PriceListProduct { PriceListId = 14, ProductId = 4, Price = 33000 , Quantity = 7 },
            new PriceListProduct { PriceListId = 14, ProductId = 5, Price = 44000 , Quantity = 25 },

            // Company 5
            new PriceListProduct { PriceListId = 15, ProductId = 10, Price =44000 , Quantity = 15 },
            new PriceListProduct { PriceListId = 15, ProductId = 1, Price = 42000 , Quantity = 5 },
            new PriceListProduct { PriceListId = 15, ProductId = 14, Price =38000 , Quantity = 2 },
            new PriceListProduct { PriceListId = 15, ProductId = 4, Price = 20000 , Quantity = 31 },
            new PriceListProduct { PriceListId = 15, ProductId = 5, Price = 49000 , Quantity = 16 },

            
            new PriceListProduct { PriceListId = 15, ProductId = 21, Price =  56000, Quantity = 20 },
            new PriceListProduct { PriceListId = 15, ProductId = 22, Price =  37000, Quantity = 22 },
            new PriceListProduct { PriceListId = 15, ProductId = 23, Price =  22000, Quantity = 24 },
            new PriceListProduct { PriceListId = 15, ProductId = 24, Price =  54000, Quantity = 26 },
            new PriceListProduct { PriceListId = 15, ProductId = 25, Price =  40000, Quantity = 28 },
            new PriceListProduct { PriceListId = 15, ProductId = 26, Price =  16000, Quantity = 30 },
            new PriceListProduct { PriceListId = 15, ProductId = 27, Price =  62000, Quantity = 32 },
            new PriceListProduct { PriceListId = 15, ProductId = 28, Price =  20000, Quantity = 34 },
            new PriceListProduct { PriceListId = 15, ProductId = 29, Price =  45000, Quantity = 36 },
            new PriceListProduct { PriceListId = 15, ProductId = 210, Price = 43000, Quantity = 38 },
            new PriceListProduct { PriceListId = 15, ProductId = 211, Price = 15000, Quantity = 40 },
            new PriceListProduct { PriceListId = 15, ProductId = 212, Price = 30000, Quantity = 42 },
            new PriceListProduct { PriceListId = 15, ProductId = 213, Price = 24000, Quantity = 44 },
            new PriceListProduct { PriceListId = 15, ProductId = 214, Price = 51000, Quantity = 46 },
            new PriceListProduct { PriceListId = 15, ProductId = 215, Price = 35000, Quantity = 48 },
            new PriceListProduct { PriceListId = 15, ProductId = 216, Price = 34000, Quantity = 50 },
            new PriceListProduct { PriceListId = 15, ProductId = 217, Price = 35000, Quantity = 52 },
            new PriceListProduct { PriceListId = 15, ProductId = 218, Price = 28000, Quantity = 54 },
            new PriceListProduct { PriceListId = 15, ProductId = 219, Price = 20000, Quantity = 56 },
            new PriceListProduct { PriceListId = 15, ProductId = 220, Price = 15000, Quantity = 58 },
            new PriceListProduct { PriceListId = 15, ProductId = 221, Price = 59000, Quantity = 60 },
            new PriceListProduct { PriceListId = 15, ProductId = 222, Price = 23000, Quantity = 62 },
            new PriceListProduct { PriceListId = 15, ProductId = 223, Price = 28000, Quantity = 64 },
            new PriceListProduct { PriceListId = 15, ProductId = 224, Price = 28000, Quantity = 66 },
            new PriceListProduct { PriceListId = 15, ProductId = 225, Price = 32000, Quantity = 68 },
            new PriceListProduct { PriceListId = 15, ProductId = 226, Price = 45000, Quantity = 70 },
            new PriceListProduct { PriceListId = 15, ProductId = 227, Price = 22000, Quantity = 72 },
            new PriceListProduct { PriceListId = 15, ProductId = 228, Price = 40000, Quantity = 74 },
            new PriceListProduct { PriceListId = 15, ProductId = 229, Price = 41000, Quantity = 76 },
            new PriceListProduct { PriceListId = 15, ProductId = 230, Price = 16000, Quantity = 78 },
            new PriceListProduct { PriceListId = 15, ProductId = 231, Price = 52000, Quantity = 80 },
            new PriceListProduct { PriceListId = 15, ProductId = 232, Price = 23000, Quantity = 82 },
            new PriceListProduct { PriceListId = 15, ProductId = 233, Price = 54000, Quantity = 84 },
            new PriceListProduct { PriceListId = 15, ProductId = 234, Price = 49000, Quantity = 86 },
            new PriceListProduct { PriceListId = 15, ProductId = 235, Price = 38000, Quantity = 88 },
            new PriceListProduct { PriceListId = 15, ProductId = 236, Price = 24000, Quantity = 90 },
            new PriceListProduct { PriceListId = 15, ProductId = 237, Price = 65000, Quantity = 92 },
            new PriceListProduct { PriceListId = 15, ProductId = 238, Price = 64000, Quantity = 94 },
            new PriceListProduct { PriceListId = 15, ProductId = 239, Price = 40000, Quantity = 96 },
            new PriceListProduct { PriceListId = 15, ProductId = 240, Price = 22000, Quantity = 98 },
            new PriceListProduct { PriceListId = 15, ProductId = 241, Price = 19000, Quantity = 100 },
            new PriceListProduct { PriceListId = 15, ProductId = 242, Price = 50000, Quantity = 102 },
            new PriceListProduct { PriceListId = 15, ProductId = 243, Price = 31000, Quantity = 104 },
            new PriceListProduct { PriceListId = 15, ProductId = 244, Price = 48000, Quantity = 106 }
            

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
            new ClientOrderProduct { ClientOrderProductId = 1, ClientOrderId = 1, ProductId = 1, Quantity = 16 },
            new ClientOrderProduct { ClientOrderProductId = 2, ClientOrderId = 1, ProductId = 2, Quantity = 21 },
            new ClientOrderProduct { ClientOrderProductId = 3, ClientOrderId = 1, ProductId = 3, Quantity = 38 },
            new ClientOrderProduct { ClientOrderProductId = 4, ClientOrderId = 1, ProductId = 4, Quantity = 20 },

            new ClientOrderProduct { ClientOrderProductId = 5, ClientOrderId = 2, ProductId = 5, Quantity = 46 },
            new ClientOrderProduct { ClientOrderProductId = 6, ClientOrderId = 2, ProductId = 6, Quantity = 35 },
            new ClientOrderProduct { ClientOrderProductId = 7, ClientOrderId = 2, ProductId = 7, Quantity = 33 },
            new ClientOrderProduct { ClientOrderProductId = 8, ClientOrderId = 2, ProductId = 8, Quantity = 3 },

            new ClientOrderProduct { ClientOrderProductId = 9, ClientOrderId = 3, ProductId = 1, Quantity = 27 },
            new ClientOrderProduct { ClientOrderProductId = 10, ClientOrderId = 3, ProductId = 15, Quantity = 47 },
            new ClientOrderProduct { ClientOrderProductId = 11, ClientOrderId = 3, ProductId = 14, Quantity = 20 },
            new ClientOrderProduct { ClientOrderProductId = 12, ClientOrderId = 3, ProductId = 13, Quantity = 45 },
            new ClientOrderProduct { ClientOrderProductId = 13, ClientOrderId = 4, ProductId = 4, Quantity = 10 },

            new ClientOrderProduct { ClientOrderProductId = 14, ClientOrderId = 4, ProductId = 12, Quantity = 22 },
            new ClientOrderProduct { ClientOrderProductId = 15, ClientOrderId = 4, ProductId = 11, Quantity = 25 },
            new ClientOrderProduct { ClientOrderProductId = 16, ClientOrderId = 4, ProductId = 10, Quantity = 32 },
            new ClientOrderProduct { ClientOrderProductId = 17, ClientOrderId = 4, ProductId = 8, Quantity = 18 },

            new ClientOrderProduct { ClientOrderProductId = 18, ClientOrderId = 5, ProductId = 9, Quantity = 15 },
            new ClientOrderProduct { ClientOrderProductId = 19, ClientOrderId = 5, ProductId = 1, Quantity = 18 },
            new ClientOrderProduct { ClientOrderProductId = 20, ClientOrderId = 5, ProductId = 2, Quantity = 26 },
            new ClientOrderProduct { ClientOrderProductId = 21, ClientOrderId = 5, ProductId = 5, Quantity = 59 },
            new ClientOrderProduct { ClientOrderProductId = 22, ClientOrderId = 5, ProductId = 4, Quantity = 34 },


            new ClientOrderProduct { ClientOrderProductId = 23, ClientOrderId = 5, ProductId = 21, Quantity =  14 },
            new ClientOrderProduct { ClientOrderProductId = 24, ClientOrderId = 5, ProductId = 21, Quantity =  47 },
            new ClientOrderProduct { ClientOrderProductId = 25, ClientOrderId = 5, ProductId = 22, Quantity =  23 },
            new ClientOrderProduct { ClientOrderProductId = 26, ClientOrderId = 5, ProductId = 22, Quantity =  15 },
            new ClientOrderProduct { ClientOrderProductId = 27, ClientOrderId = 5, ProductId = 23, Quantity =  5 },
            new ClientOrderProduct { ClientOrderProductId = 28, ClientOrderId = 5, ProductId = 23, Quantity =  8 },
            new ClientOrderProduct { ClientOrderProductId = 29, ClientOrderId = 5, ProductId = 24, Quantity =  19 },
            new ClientOrderProduct { ClientOrderProductId = 30, ClientOrderId = 5, ProductId = 24, Quantity =  16 },
            new ClientOrderProduct { ClientOrderProductId = 31, ClientOrderId = 5, ProductId = 25, Quantity =  13 },
            new ClientOrderProduct { ClientOrderProductId = 32, ClientOrderId = 5, ProductId = 25, Quantity =  12 },
            new ClientOrderProduct { ClientOrderProductId = 33, ClientOrderId = 5, ProductId = 26, Quantity =  35 },
            new ClientOrderProduct { ClientOrderProductId = 34, ClientOrderId = 5, ProductId = 26, Quantity =  1 },
            new ClientOrderProduct { ClientOrderProductId = 35, ClientOrderId = 5, ProductId = 27, Quantity =  47 },
            new ClientOrderProduct { ClientOrderProductId = 36, ClientOrderId = 5, ProductId = 27, Quantity =  29 },
            new ClientOrderProduct { ClientOrderProductId = 37, ClientOrderId = 5, ProductId = 28, Quantity =  45 },
            new ClientOrderProduct { ClientOrderProductId = 38, ClientOrderId = 5, ProductId = 28, Quantity =  15 },
            new ClientOrderProduct { ClientOrderProductId = 39, ClientOrderId = 5, ProductId = 29, Quantity =  10 },
            new ClientOrderProduct { ClientOrderProductId = 40, ClientOrderId = 5, ProductId = 29, Quantity =  22 },
            new ClientOrderProduct { ClientOrderProductId = 41, ClientOrderId = 5, ProductId = 210, Quantity = 6 },
            new ClientOrderProduct { ClientOrderProductId = 42, ClientOrderId = 5, ProductId = 210, Quantity = 29 },
            new ClientOrderProduct { ClientOrderProductId = 43, ClientOrderId = 5, ProductId = 211, Quantity = 45 },

            new ClientOrderProduct { ClientOrderProductId = 44, ClientOrderId = 5, ProductId = 211, Quantity = 15 },
            new ClientOrderProduct { ClientOrderProductId = 45, ClientOrderId = 5, ProductId = 212, Quantity = 10 },
            new ClientOrderProduct { ClientOrderProductId = 46, ClientOrderId = 5, ProductId = 212, Quantity = 22 },
            new ClientOrderProduct { ClientOrderProductId = 47, ClientOrderId = 5, ProductId = 213, Quantity = 6 },
            new ClientOrderProduct { ClientOrderProductId = 48, ClientOrderId = 5, ProductId = 213, Quantity = 29 },
            new ClientOrderProduct { ClientOrderProductId = 49, ClientOrderId = 5, ProductId = 214, Quantity = 21 },
            new ClientOrderProduct { ClientOrderProductId = 50, ClientOrderId = 5, ProductId = 214, Quantity = 12 },
            new ClientOrderProduct { ClientOrderProductId = 51, ClientOrderId = 5, ProductId = 215, Quantity = 10 },
            new ClientOrderProduct { ClientOrderProductId = 52, ClientOrderId = 5, ProductId = 215, Quantity = 44 },
            new ClientOrderProduct { ClientOrderProductId = 53, ClientOrderId = 5, ProductId = 216, Quantity = 26 },
            new ClientOrderProduct { ClientOrderProductId = 54, ClientOrderId = 5, ProductId = 216, Quantity = 49 },
            new ClientOrderProduct { ClientOrderProductId = 55, ClientOrderId = 5, ProductId = 217, Quantity = 31 },
            new ClientOrderProduct { ClientOrderProductId = 56, ClientOrderId = 5, ProductId = 217, Quantity = 12 },
            new ClientOrderProduct { ClientOrderProductId = 57, ClientOrderId = 5, ProductId = 218, Quantity = 7 },
            new ClientOrderProduct { ClientOrderProductId = 58, ClientOrderId = 5, ProductId = 218, Quantity = 5 },
            new ClientOrderProduct { ClientOrderProductId = 59, ClientOrderId = 5, ProductId = 219, Quantity = 21 },
            new ClientOrderProduct { ClientOrderProductId = 60, ClientOrderId = 5, ProductId = 219, Quantity = 29 },
            new ClientOrderProduct { ClientOrderProductId = 61, ClientOrderId = 5, ProductId = 220, Quantity = 2 },
            new ClientOrderProduct { ClientOrderProductId = 62, ClientOrderId = 5, ProductId = 220, Quantity = 39 },
            new ClientOrderProduct { ClientOrderProductId = 63, ClientOrderId = 5, ProductId = 221, Quantity = 48 },
            new ClientOrderProduct { ClientOrderProductId = 64, ClientOrderId = 5, ProductId = 221, Quantity = 28 },
            new ClientOrderProduct { ClientOrderProductId = 65, ClientOrderId = 5, ProductId = 222, Quantity = 16 },

            new ClientOrderProduct { ClientOrderProductId = 66, ClientOrderId = 5, ProductId = 222, Quantity = 43 },
            new ClientOrderProduct { ClientOrderProductId = 67, ClientOrderId = 5, ProductId = 223, Quantity = 31 },
            new ClientOrderProduct { ClientOrderProductId = 68, ClientOrderId = 5, ProductId = 223, Quantity = 32 },
            new ClientOrderProduct { ClientOrderProductId = 69, ClientOrderId = 5, ProductId = 224, Quantity = 21 },
            new ClientOrderProduct { ClientOrderProductId = 70, ClientOrderId = 5, ProductId = 224, Quantity = 12 },
            new ClientOrderProduct { ClientOrderProductId = 71, ClientOrderId = 5, ProductId = 225, Quantity = 27 },
            new ClientOrderProduct { ClientOrderProductId = 72, ClientOrderId = 5, ProductId = 225, Quantity = 31 },
            new ClientOrderProduct { ClientOrderProductId = 73, ClientOrderId = 5, ProductId = 226, Quantity = 49 },
            new ClientOrderProduct { ClientOrderProductId = 74, ClientOrderId = 5, ProductId = 226, Quantity = 5 },
            new ClientOrderProduct { ClientOrderProductId = 75, ClientOrderId = 5, ProductId = 227, Quantity = 40 },
            new ClientOrderProduct { ClientOrderProductId = 76, ClientOrderId = 5, ProductId = 227, Quantity = 52 },
            new ClientOrderProduct { ClientOrderProductId = 77, ClientOrderId = 5, ProductId = 228, Quantity = 24 },
            new ClientOrderProduct { ClientOrderProductId = 78, ClientOrderId = 5, ProductId = 228, Quantity = 27 },
            new ClientOrderProduct { ClientOrderProductId = 79, ClientOrderId = 5, ProductId = 229, Quantity = 17 },
            new ClientOrderProduct { ClientOrderProductId = 80, ClientOrderId = 5, ProductId = 229, Quantity = 23 },
            new ClientOrderProduct { ClientOrderProductId = 81, ClientOrderId = 5, ProductId = 230, Quantity = 38 },
            new ClientOrderProduct { ClientOrderProductId = 82, ClientOrderId = 5, ProductId = 230, Quantity = 1 },
            new ClientOrderProduct { ClientOrderProductId = 83, ClientOrderId = 5, ProductId = 231, Quantity = 52 },
            new ClientOrderProduct { ClientOrderProductId = 84, ClientOrderId = 5, ProductId = 231, Quantity = 9 },
            new ClientOrderProduct { ClientOrderProductId = 85, ClientOrderId = 5, ProductId = 232, Quantity = 2 },
            new ClientOrderProduct { ClientOrderProductId = 86, ClientOrderId = 5, ProductId = 232, Quantity = 7 },

            new ClientOrderProduct { ClientOrderProductId = 87, ClientOrderId = 5, ProductId = 233, Quantity = 29 },
            new ClientOrderProduct { ClientOrderProductId = 88, ClientOrderId = 5, ProductId = 233, Quantity = 10 },
            new ClientOrderProduct { ClientOrderProductId = 89, ClientOrderId = 5, ProductId = 234, Quantity = 49 },
            new ClientOrderProduct { ClientOrderProductId = 90, ClientOrderId = 5, ProductId = 234, Quantity = 37 },
            new ClientOrderProduct { ClientOrderProductId = 91, ClientOrderId = 5, ProductId = 235, Quantity = 1 },
            new ClientOrderProduct { ClientOrderProductId = 92, ClientOrderId = 5, ProductId = 235, Quantity = 12 },
            new ClientOrderProduct { ClientOrderProductId = 93, ClientOrderId = 5, ProductId = 236, Quantity = 28},
            new ClientOrderProduct { ClientOrderProductId = 94, ClientOrderId = 5, ProductId = 236, Quantity = 18 },
            new ClientOrderProduct { ClientOrderProductId = 95, ClientOrderId = 5, ProductId = 237, Quantity = 58 },
            new ClientOrderProduct { ClientOrderProductId = 96, ClientOrderId = 5, ProductId = 237, Quantity = 31 },
            new ClientOrderProduct { ClientOrderProductId = 97, ClientOrderId = 5, ProductId = 238, Quantity = 22 },
            new ClientOrderProduct { ClientOrderProductId = 98, ClientOrderId = 5, ProductId = 238, Quantity = 13 },
            new ClientOrderProduct { ClientOrderProductId = 99, ClientOrderId = 5, ProductId = 239, Quantity = 48 },
            new ClientOrderProduct { ClientOrderProductId = 100, ClientOrderId = 5, ProductId = 239, Quantity =1 },
            new ClientOrderProduct { ClientOrderProductId = 101, ClientOrderId = 5, ProductId = 240, Quantity =13 },
            new ClientOrderProduct { ClientOrderProductId = 102, ClientOrderId = 5, ProductId = 240, Quantity =18 },
            new ClientOrderProduct { ClientOrderProductId = 103, ClientOrderId = 5, ProductId = 241, Quantity =19 },
            new ClientOrderProduct { ClientOrderProductId = 104, ClientOrderId = 5, ProductId = 241, Quantity =33 },
            new ClientOrderProduct { ClientOrderProductId = 105, ClientOrderId = 5, ProductId = 242, Quantity =22 }
            //new ClientOrderProduct { ClientOrderProductId = 106, ClientOrderId = 5, ProductId = 242, Quantity = },
            //new ClientOrderProduct { ClientOrderProductId = 107, ClientOrderId = 5, ProductId = 243, Quantity = },
            //new ClientOrderProduct { ClientOrderProductId = 108, ClientOrderId = 5, ProductId = 243, Quantity = },
            //new ClientOrderProduct { ClientOrderProductId = 109, ClientOrderId = 5, ProductId = 244, Quantity = },
            //new ClientOrderProduct { ClientOrderProductId = 110, ClientOrderId = 5, ProductId = 244, Quantity = }

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
