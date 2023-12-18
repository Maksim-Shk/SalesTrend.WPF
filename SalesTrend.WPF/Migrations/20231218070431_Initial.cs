using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SalesTrend.WPF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ContactPersonFullName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Individual",
                columns: table => new
                {
                    IndividualId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Surname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TaxNumber = table.Column<int>(type: "integer", nullable: false),
                    PassportSerial = table.Column<int>(type: "integer", nullable: false),
                    PassportNumber = table.Column<int>(type: "integer", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IssuedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individual", x => x.IndividualId);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntity",
                columns: table => new
                {
                    LegalEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TaxNumber = table.Column<int>(type: "integer", nullable: false),
                    ContactPersonFullName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntity", x => x.LegalEntityId);
                });

            migrationBuilder.CreateTable(
                name: "LocalityType",
                columns: table => new
                {
                    LocalityTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalityType", x => x.LocalityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ProductTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ProductTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StreetType",
                columns: table => new
                {
                    StreetTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetType", x => x.StreetTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PriceList",
                columns: table => new
                {
                    PriceListId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceList", x => x.PriceListId);
                    table.ForeignKey(
                        name: "FK_PriceList_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientOrder",
                columns: table => new
                {
                    ClientOrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOrder", x => x.ClientOrderId);
                    table.ForeignKey(
                        name: "FK_ClientOrder_Individual_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Individual",
                        principalColumn: "IndividualId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientOrder_LegalEntity_ClientId",
                        column: x => x.ClientId,
                        principalTable: "LegalEntity",
                        principalColumn: "LegalEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    PhoneId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoneId);
                    table.ForeignKey(
                        name: "FK_Phone_Company_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phone_LegalEntity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "LegalEntity",
                        principalColumn: "LegalEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locality",
                columns: table => new
                {
                    LocalityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LocalityTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locality", x => x.LocalityId);
                    table.ForeignKey(
                        name: "FK_Locality_LocalityType_LocalityTypeId",
                        column: x => x.LocalityTypeId,
                        principalTable: "LocalityType",
                        principalColumn: "LocalityTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Article = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ProductTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Street",
                columns: table => new
                {
                    StreetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StreetTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.StreetId);
                    table.ForeignKey(
                        name: "FK_Street_StreetType_StreetTypeId",
                        column: x => x.StreetTypeId,
                        principalTable: "StreetType",
                        principalColumn: "StreetTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientOrderProduct",
                columns: table => new
                {
                    ClientOrderProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientOrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOrderProduct", x => x.ClientOrderProductId);
                    table.ForeignKey(
                        name: "FK_ClientOrderProduct_ClientOrder_ClientOrderId",
                        column: x => x.ClientOrderId,
                        principalTable: "ClientOrder",
                        principalColumn: "ClientOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientOrderProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListProduct",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    PriceListId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListProduct", x => new { x.ProductId, x.PriceListId });
                    table.ForeignKey(
                        name: "FK_PriceListProduct_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceList",
                        principalColumn: "PriceListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceListProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    House = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Apartment = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LocalityId = table.Column<int>(type: "integer", nullable: false),
                    StreetId = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    IndividualId = table.Column<Guid>(type: "uuid", nullable: true),
                    LegalEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId");
                    table.ForeignKey(
                        name: "FK_Address_Individual_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individual",
                        principalColumn: "IndividualId");
                    table.ForeignKey(
                        name: "FK_Address_LegalEntity_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalTable: "LegalEntity",
                        principalColumn: "LegalEntityId");
                    table.ForeignKey(
                        name: "FK_Address_Locality_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Locality",
                        principalColumn: "LocalityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Street_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Street",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "ContactPersonFullName", "Email", "Name", "Url" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Иван Иванович Иванов", "email1@mail.ru", "ООО ТехноТрейд 1", "https://some-site1.com" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Петр Петрович Петров", "email2@mail.ru", "ООО ТехноТрейд 2", "https://some-site2.com" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Сергей Сергеевич Сергеев", "email3@mail.ru", "ООО ТехноТрейд 3", "https://some-site3.com" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Алексей Алексеевич Алексеев", "email4@mail.ru", "ООО ТехноТрейд 4", "https://some-site4.com" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Дмитрий Дмитриевич Дмитриев", "email5@mail.ru", "ООО ТехноТрейд 5", "https://some-site5.com" }
                });

            migrationBuilder.InsertData(
                table: "Individual",
                columns: new[] { "IndividualId", "IssueDate", "IssuedBy", "Name", "PassportNumber", "PassportSerial", "Patronymic", "Surname", "TaxNumber" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4582), "УФМС", "Иван", 567890, 1234, "Иванович", "Иванов", 123456789 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4584), "УФМС", "Петр", 123456, 5678, "Петрович", "Петров", 987654321 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4586), "УФМС", "Сидор", 112233, 9101, "Сидорович", "Сидоров", 567890123 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4588), "УФМС", "Александр", 161718, 1415, "Александрович", "Александров", 234567890 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4589), "УФМС", "Дмитрий", 212223, 1920, "Дмитриевич", "Дмитриев", 678901234 }
                });

            migrationBuilder.InsertData(
                table: "LegalEntity",
                columns: new[] { "LegalEntityId", "ContactPersonFullName", "Email", "Name", "ShortName", "TaxNumber" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Иванов Иван Иванович", "ivanov@example.com", "ООО ТехноТрейд", null, 123456789 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Петров Петр Петрович", "petrov@example.com", "ЗАО ЭлектроМагазин", null, 987654321 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Сидоров Сидор Сидорович", "sidorov@example.com", "ПАО Инновационные Решения", null, 567890123 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Александров Александр Александрович", "alexandrov@example.com", "ООО Торговый Элит", null, 234567890 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Дмитриев Дмитрий Дмитриевич", "dmitriev@example.com", "ЗАО Продвинутые Технологии", null, 678901234 }
                });

            migrationBuilder.InsertData(
                table: "LocalityType",
                columns: new[] { "LocalityTypeId", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Город", "г." },
                    { 2, "Поселок", "п." },
                    { 3, "Деревня", "д." },
                    { 4, "Хутор", "х." },
                    { 5, "Село", "с." }
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "ProductTypeId", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Смартфоны", "Смартфоны" },
                    { 2, "Товары для дома", "Товары для дома" },
                    { 3, "Одежда", "Одежда" },
                    { 4, "Ноутбуки", "Ноутбуки" },
                    { 5, "Прочая электроника", "Электроника" }
                });

            migrationBuilder.InsertData(
                table: "StreetType",
                columns: new[] { "StreetTypeId", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Улица", "ул." },
                    { 2, "Проспект", "пр." },
                    { 3, "Бульвар", "бул." },
                    { 4, "Переулок", "пер." },
                    { 5, "Проезд", "пр-д" }
                });

            migrationBuilder.InsertData(
                table: "ClientOrder",
                columns: new[] { "ClientOrderId", "ClientId", "OrderDate" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4749) },
                    { 2, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4751) },
                    { 3, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4753) },
                    { 4, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4754) },
                    { 5, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4755) }
                });

            migrationBuilder.InsertData(
                table: "Locality",
                columns: new[] { "LocalityId", "LocalityTypeId", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, 1, "Оренбург", "Орен." },
                    { 2, 2, "Вязовка", "Вязовка" },
                    { 3, 3, "Березовка", "Березовк." },
                    { 4, 4, "Заборье", "Заборье" },
                    { 5, 5, "Черничка", "Чернич." }
                });

            migrationBuilder.InsertData(
                table: "Phone",
                columns: new[] { "PhoneId", "EntityId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000001"), "+78005553535" },
                    { 2, new Guid("00000000-0000-0000-0000-000000000002"), "+78005553535" },
                    { 3, new Guid("00000000-0000-0000-0000-000000000003"), "+78005553535" },
                    { 4, new Guid("00000000-0000-0000-0000-000000000004"), "+78005553535" },
                    { 5, new Guid("00000000-0000-0000-0000-000000000005"), "+78005553535" },
                    { 6, new Guid("00000000-0000-0000-0000-000000000001"), "+78005553535" },
                    { 7, new Guid("00000000-0000-0000-0000-000000000002"), "+78005553535" },
                    { 8, new Guid("00000000-0000-0000-0000-000000000003"), "+78005553535" },
                    { 9, new Guid("00000000-0000-0000-0000-000000000004"), "+78005553535" },
                    { 10, new Guid("00000000-0000-0000-0000-000000000005"), "+78005553535" },
                    { 11, new Guid("00000000-0000-0000-0000-000000000001"), "+78005553535" },
                    { 12, new Guid("00000000-0000-0000-0000-000000000002"), "+78005553535" },
                    { 13, new Guid("00000000-0000-0000-0000-000000000003"), "+78005553535" },
                    { 14, new Guid("00000000-0000-0000-0000-000000000004"), "+78005553535" },
                    { 15, new Guid("00000000-0000-0000-0000-000000000005"), "+78005553535" }
                });

            migrationBuilder.InsertData(
                table: "PriceList",
                columns: new[] { "PriceListId", "CompanyId", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4369) },
                    { 2, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4373) },
                    { 3, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 10, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4386) },
                    { 4, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4388) },
                    { 5, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 11, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4389) },
                    { 6, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 10, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4390) },
                    { 7, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4392) },
                    { 8, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 11, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4393) },
                    { 9, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 10, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4394) },
                    { 10, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4395) },
                    { 11, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 11, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4397) },
                    { 12, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 10, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4398) },
                    { 13, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4399) },
                    { 14, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 11, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4400) },
                    { 15, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 10, 18, 7, 4, 31, 143, DateTimeKind.Utc).AddTicks(4436) }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Article", "Name", "ProductTypeId" },
                values: new object[,]
                {
                    { 1, "1KF42", "Смартфон", 1 },
                    { 2, "2KF42", "Ноутбук", 4 },
                    { 3, "3KF42", "Смарт-телевизор", 5 },
                    { 4, "4KF42", "Цифровая камера", 5 },
                    { 5, "5KF42", "Беспроводные наушники", 5 },
                    { 6, "6KF42", "Фитнес-трекер", 5 },
                    { 7, "7KF42", "Робот-пылесос", 5 },
                    { 8, "8KF42", "Смарт-термостат", 5 },
                    { 9, "9KF42", "Дрон", 5 },
                    { 10, "10KF42", "Игровая консоль", 5 },
                    { 11, "11KF42", "Наушники", 5 },
                    { 12, "12KF42", "Планшет", 5 },
                    { 13, "13KF42", "Смарт-часы", 5 },
                    { 14, "14KF42", "Камера видеонаблюдения", 5 },
                    { 15, "15KF42", "Электрическая зубная щетка", 5 }
                });

            migrationBuilder.InsertData(
                table: "Street",
                columns: new[] { "StreetId", "Name", "ShortName", "StreetTypeId" },
                values: new object[,]
                {
                    { 1, "Новая", "ул.", 1 },
                    { 2, "Мира", "пр.", 2 },
                    { 3, "Центральный", "бул.", 3 },
                    { 4, "Молодежный", "пер.", 4 },
                    { 5, "Луговой", "пр-д", 5 }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Apartment", "CompanyId", "House", "IndividualId", "LegalEntityId", "LocalityId", "StreetId" },
                values: new object[,]
                {
                    { 1, "114", new Guid("00000000-0000-0000-0000-000000000001"), "1241", null, null, 1, 1 },
                    { 2, "124", new Guid("00000000-0000-0000-0000-000000000002"), "1242", null, null, 1, 1 },
                    { 3, "141", new Guid("00000000-0000-0000-0000-000000000003"), "1234", null, null, 1, 1 },
                    { 4, "134", new Guid("00000000-0000-0000-0000-000000000004"), "1424", null, null, 2, 2 },
                    { 5, "141", new Guid("00000000-0000-0000-0000-000000000005"), "1524", null, null, 2, 2 },
                    { 6, "144", null, "1624", null, new Guid("00000000-0000-0000-0000-000000000001"), 2, 2 },
                    { 7, "14", null, "1274", null, new Guid("00000000-0000-0000-0000-000000000002"), 3, 3 },
                    { 8, "514", null, "1124", null, new Guid("00000000-0000-0000-0000-000000000003"), 3, 3 },
                    { 9, "124", null, "1242", null, new Guid("00000000-0000-0000-0000-000000000004"), 3, 3 },
                    { 10, "214", null, "124", null, new Guid("00000000-0000-0000-0000-000000000005"), 4, 4 },
                    { 11, "714", null, "124", new Guid("00000000-0000-0000-0000-000000000001"), null, 4, 4 },
                    { 12, "814", null, "124", new Guid("00000000-0000-0000-0000-000000000002"), null, 4, 4 },
                    { 13, "14", null, "444", new Guid("00000000-0000-0000-0000-000000000003"), null, 5, 5 },
                    { 14, "194", null, "111", new Guid("00000000-0000-0000-0000-000000000004"), null, 5, 5 },
                    { 15, "149", null, "222", new Guid("00000000-0000-0000-0000-000000000005"), null, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "ClientOrderProduct",
                columns: new[] { "ClientOrderProductId", "ClientOrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 5 },
                    { 2, 1, 2, 2 },
                    { 3, 1, 3, 6 },
                    { 4, 1, 4, 1 },
                    { 5, 2, 5, 2 },
                    { 6, 2, 6, 5 },
                    { 7, 2, 7, 8 },
                    { 8, 2, 8, 1 },
                    { 9, 3, 1, 2 },
                    { 10, 3, 15, 5 },
                    { 11, 3, 14, 8 },
                    { 12, 3, 13, 1 },
                    { 13, 4, 12, 2 },
                    { 14, 4, 11, 5 },
                    { 15, 4, 10, 8 },
                    { 16, 4, 8, 1 },
                    { 17, 5, 9, 2 },
                    { 18, 5, 1, 5 },
                    { 19, 5, 2, 8 },
                    { 20, 5, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "PriceListProduct",
                columns: new[] { "PriceListId", "ProductId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 100m, 5 },
                    { 2, 1, 120m, 1 },
                    { 3, 1, 200m, 15 },
                    { 7, 1, 110m, 7 },
                    { 8, 1, 120m, 1 },
                    { 9, 1, 200m, 15 },
                    { 13, 1, 160m, 6 },
                    { 14, 1, 110m, 18 },
                    { 15, 1, 130m, 5 },
                    { 1, 2, 150m, 8 },
                    { 2, 2, 110m, 18 },
                    { 3, 2, 130m, 5 },
                    { 10, 2, 130m, 4 },
                    { 11, 2, 110m, 7 },
                    { 12, 2, 150m, 31 },
                    { 1, 3, 200m, 10 },
                    { 2, 3, 250m, 11 },
                    { 3, 3, 110m, 2 },
                    { 10, 3, 110m, 7 },
                    { 11, 3, 120m, 1 },
                    { 12, 3, 200m, 15 },
                    { 1, 4, 120m, 3 },
                    { 2, 4, 110m, 7 },
                    { 3, 4, 150m, 31 },
                    { 13, 4, 130m, 4 },
                    { 14, 4, 110m, 7 },
                    { 15, 4, 150m, 31 },
                    { 1, 5, 80m, 15 },
                    { 2, 5, 180m, 25 },
                    { 3, 5, 110m, 16 },
                    { 13, 5, 90m, 20 },
                    { 14, 5, 180m, 25 },
                    { 15, 5, 110m, 16 },
                    { 7, 6, 160m, 6 },
                    { 8, 6, 110m, 18 },
                    { 9, 6, 130m, 5 },
                    { 10, 6, 90m, 20 },
                    { 11, 6, 180m, 25 },
                    { 12, 6, 110m, 16 },
                    { 7, 7, 180m, 12 },
                    { 8, 7, 250m, 11 },
                    { 9, 7, 110m, 2 },
                    { 7, 8, 130m, 4 },
                    { 8, 8, 110m, 7 },
                    { 9, 8, 150m, 31 },
                    { 7, 9, 90m, 20 },
                    { 8, 9, 180m, 25 },
                    { 9, 9, 110m, 16 },
                    { 10, 10, 160m, 6 },
                    { 11, 10, 110m, 18 },
                    { 12, 10, 130m, 5 },
                    { 13, 10, 110m, 7 },
                    { 14, 10, 120m, 1 },
                    { 15, 10, 200m, 15 },
                    { 4, 11, 110m, 7 },
                    { 5, 11, 120m, 1 },
                    { 6, 11, 200m, 15 },
                    { 10, 11, 180m, 12 },
                    { 11, 11, 250m, 11 },
                    { 12, 11, 110m, 2 },
                    { 4, 12, 160m, 6 },
                    { 5, 12, 110m, 18 },
                    { 6, 12, 130m, 5 },
                    { 4, 13, 180m, 12 },
                    { 5, 13, 250m, 11 },
                    { 6, 13, 110m, 2 },
                    { 4, 14, 130m, 4 },
                    { 5, 14, 110m, 7 },
                    { 6, 14, 150m, 31 },
                    { 13, 14, 180m, 12 },
                    { 14, 14, 250m, 11 },
                    { 15, 14, 110m, 2 },
                    { 4, 15, 90m, 20 },
                    { 5, 15, 180m, 25 },
                    { 6, 15, 110m, 16 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CompanyId",
                table: "Address",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_IndividualId",
                table: "Address",
                column: "IndividualId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_LegalEntityId",
                table: "Address",
                column: "LegalEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_LocalityId",
                table: "Address",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StreetId",
                table: "Address",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrder_ClientId",
                table: "ClientOrder",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrderProduct_ClientOrderId",
                table: "ClientOrderProduct",
                column: "ClientOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrderProduct_ProductId",
                table: "ClientOrderProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Locality_LocalityTypeId",
                table: "Locality",
                column: "LocalityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_EntityId",
                table: "Phone",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceList_CompanyId",
                table: "PriceList",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListProduct_PriceListId",
                table: "PriceListProduct",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Street_StreetTypeId",
                table: "Street",
                column: "StreetTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ClientOrderProduct");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "PriceListProduct");

            migrationBuilder.DropTable(
                name: "Locality");

            migrationBuilder.DropTable(
                name: "Street");

            migrationBuilder.DropTable(
                name: "ClientOrder");

            migrationBuilder.DropTable(
                name: "PriceList");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "LocalityType");

            migrationBuilder.DropTable(
                name: "StreetType");

            migrationBuilder.DropTable(
                name: "Individual");

            migrationBuilder.DropTable(
                name: "LegalEntity");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}
