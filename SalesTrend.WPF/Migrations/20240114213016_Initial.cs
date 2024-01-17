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
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(532), "УФМС", "Иван", 567890, 1234, "Иванович", "Иванов", 123456789 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(535), "УФМС", "Петр", 123456, 5678, "Петрович", "Петров", 987654321 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(537), "УФМС", "Сидор", 112233, 9101, "Сидорович", "Сидоров", 567890123 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(538), "УФМС", "Александр", 161718, 1415, "Александрович", "Александров", 234567890 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(540), "УФМС", "Дмитрий", 212223, 1920, "Дмитриевич", "Дмитриев", 678901234 }
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
                    { 1, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(663) },
                    { 2, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(665) },
                    { 3, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(666) },
                    { 4, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(667) },
                    { 5, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(668) }
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
                    { 1, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(353) },
                    { 2, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 12, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(357) },
                    { 3, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(363) },
                    { 4, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(365) },
                    { 5, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(366) },
                    { 6, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 11, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(368) },
                    { 7, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(369) },
                    { 8, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(370) },
                    { 9, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 11, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(372) },
                    { 10, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(373) },
                    { 11, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 12, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(374) },
                    { 12, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 11, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(376) },
                    { 13, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(377) },
                    { 14, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(378) },
                    { 15, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 11, 14, 21, 30, 16, 416, DateTimeKind.Utc).AddTicks(379) }
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
                    { 15, "15KF42", "Электрическая зубная щетка", 5 },
                    { 21, "1KF42", "Смартфон", 1 },
                    { 22, "2KF42", "Ноутбук", 4 },
                    { 23, "3KF42", "Планшет", 5 },
                    { 24, "4KF42", "Телевизор", 5 },
                    { 25, "5KF42", "Наушники", 5 },
                    { 26, "6KF42", "Камера", 5 },
                    { 27, "7KF42", "Фотоаппарат", 5 },
                    { 28, "8KF42", "Монитор", 5 },
                    { 29, "9KF42", "Принтер", 5 },
                    { 210, "10KF42", "Роутер", 5 },
                    { 211, "11KF42", "Смарт-часы", 5 },
                    { 212, "12KF42", "Электронная книга", 5 },
                    { 213, "13KF42", "Умный домофон", 5 },
                    { 214, "14KF42", "Аккумулятор", 5 },
                    { 215, "15KF42", "Портативная зарядка", 5 },
                    { 216, "16KF42", "Гарнитура", 5 },
                    { 217, "17KF42", "Bluetooth-динамик", 5 },
                    { 218, "18KF42", "Веб-камера", 5 },
                    { 219, "19KF42", "Флеш-накопитель", 5 },
                    { 220, "20KF42", "SSD-накопитель", 5 },
                    { 221, "21KF42", "Внешний жесткий диск", 5 },
                    { 222, "22KF42", "Игровая консоль", 5 },
                    { 223, "23KF42", "VR-шлем", 5 },
                    { 224, "24KF42", "Смарт-телевизор", 5 },
                    { 225, "25KF42", "Микрофон", 5 },
                    { 226, "26KF42", "Тонер-картридж", 5 },
                    { 227, "27KF42", "Манипулятор (мышь, трекбол)", 5 },
                    { 228, "28KF42", "Адаптер Wi-Fi", 5 },
                    { 229, "29KF42", "Видеокарта", 5 },
                    { 230, "30KF42", "Процессор", 5 },
                    { 231, "31KF42", "Материнская плата", 5 },
                    { 232, "32KF42", "Оперативная память (RAM)", 5 },
                    { 233, "33KF42", "Компьютерная мышь", 5 },
                    { 234, "34KF42", "Клавиатура", 5 },
                    { 235, "35KF42", "USB-хаб", 5 },
                    { 236, "36KF42", "HDMI-кабель", 5 },
                    { 237, "37KF42", "Сетевой фильтр", 5 },
                    { 238, "38KF42", "Электронный браслет", 5 },
                    { 239, "39KF42", "Внешний оптический привод", 5 },
                    { 240, "40KF42", "Игровой контроллер", 5 },
                    { 241, "41KF42", "Стабилизатор напряжения", 5 },
                    { 242, "42KF42", "Термопаста", 5 },
                    { 243, "43KF42", "Солнечная батарея", 5 },
                    { 244, "44KF42", "GPS-навигатор", 5 }
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
                    { 1, 1, 1, 16 },
                    { 2, 1, 2, 21 },
                    { 3, 1, 3, 38 },
                    { 4, 1, 4, 20 },
                    { 5, 2, 5, 46 },
                    { 6, 2, 6, 35 },
                    { 7, 2, 7, 33 },
                    { 8, 2, 8, 3 },
                    { 9, 3, 1, 27 },
                    { 10, 3, 15, 47 },
                    { 11, 3, 14, 20 },
                    { 12, 3, 13, 45 },
                    { 13, 4, 4, 10 },
                    { 14, 4, 12, 22 },
                    { 15, 4, 11, 25 },
                    { 16, 4, 10, 32 },
                    { 17, 4, 8, 18 },
                    { 18, 5, 9, 15 },
                    { 19, 5, 1, 18 },
                    { 20, 5, 2, 26 },
                    { 21, 5, 5, 59 },
                    { 22, 5, 4, 34 },
                    { 23, 5, 21, 14 },
                    { 24, 5, 21, 47 },
                    { 25, 5, 22, 23 },
                    { 26, 5, 22, 15 },
                    { 27, 5, 23, 5 },
                    { 28, 5, 23, 8 },
                    { 29, 5, 24, 19 },
                    { 30, 5, 24, 16 },
                    { 31, 5, 25, 13 },
                    { 32, 5, 25, 12 },
                    { 33, 5, 26, 35 },
                    { 34, 5, 26, 1 },
                    { 35, 5, 27, 47 },
                    { 36, 5, 27, 29 },
                    { 37, 5, 28, 45 },
                    { 38, 5, 28, 15 },
                    { 39, 5, 29, 10 },
                    { 40, 5, 29, 22 },
                    { 41, 5, 210, 6 },
                    { 42, 5, 210, 29 },
                    { 43, 5, 211, 45 },
                    { 44, 5, 211, 15 },
                    { 45, 5, 212, 10 },
                    { 46, 5, 212, 22 },
                    { 47, 5, 213, 6 },
                    { 48, 5, 213, 29 },
                    { 49, 5, 214, 21 },
                    { 50, 5, 214, 12 },
                    { 51, 5, 215, 10 },
                    { 52, 5, 215, 44 },
                    { 53, 5, 216, 26 },
                    { 54, 5, 216, 49 },
                    { 55, 5, 217, 31 },
                    { 56, 5, 217, 12 },
                    { 57, 5, 218, 7 },
                    { 58, 5, 218, 5 },
                    { 59, 5, 219, 21 },
                    { 60, 5, 219, 29 },
                    { 61, 5, 220, 2 },
                    { 62, 5, 220, 39 },
                    { 63, 5, 221, 48 },
                    { 64, 5, 221, 28 },
                    { 65, 5, 222, 16 },
                    { 66, 5, 222, 43 },
                    { 67, 5, 223, 31 },
                    { 68, 5, 223, 32 },
                    { 69, 5, 224, 21 },
                    { 70, 5, 224, 12 },
                    { 71, 5, 225, 27 },
                    { 72, 5, 225, 31 },
                    { 73, 5, 226, 49 },
                    { 74, 5, 226, 5 },
                    { 75, 5, 227, 40 },
                    { 76, 5, 227, 52 },
                    { 77, 5, 228, 24 },
                    { 78, 5, 228, 27 },
                    { 79, 5, 229, 17 },
                    { 80, 5, 229, 23 },
                    { 81, 5, 230, 38 },
                    { 82, 5, 230, 1 },
                    { 83, 5, 231, 52 },
                    { 84, 5, 231, 9 },
                    { 85, 5, 232, 2 },
                    { 86, 5, 232, 7 },
                    { 87, 5, 233, 29 },
                    { 88, 5, 233, 10 },
                    { 89, 5, 234, 49 },
                    { 90, 5, 234, 37 },
                    { 91, 5, 235, 1 },
                    { 92, 5, 235, 12 },
                    { 93, 5, 236, 28 },
                    { 94, 5, 236, 18 },
                    { 95, 5, 237, 58 },
                    { 96, 5, 237, 31 },
                    { 97, 5, 238, 22 },
                    { 98, 5, 238, 13 },
                    { 99, 5, 239, 48 },
                    { 100, 5, 239, 1 },
                    { 101, 5, 240, 13 },
                    { 102, 5, 240, 18 },
                    { 103, 5, 241, 19 },
                    { 104, 5, 241, 33 },
                    { 105, 5, 242, 22 }
                });

            migrationBuilder.InsertData(
                table: "PriceListProduct",
                columns: new[] { "PriceListId", "ProductId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 10000m, 5 },
                    { 2, 1, 12000m, 1 },
                    { 3, 1, 20000m, 15 },
                    { 7, 1, 11000m, 7 },
                    { 8, 1, 12000m, 1 },
                    { 9, 1, 20000m, 15 },
                    { 13, 1, 26000m, 6 },
                    { 14, 1, 42000m, 18 },
                    { 15, 1, 42000m, 5 },
                    { 1, 2, 15000m, 8 },
                    { 2, 2, 11000m, 18 },
                    { 3, 2, 13000m, 5 },
                    { 10, 2, 46000m, 4 },
                    { 11, 2, 55000m, 7 },
                    { 12, 2, 33000m, 31 },
                    { 1, 3, 20000m, 10 },
                    { 2, 3, 33000m, 11 },
                    { 3, 3, 11000m, 2 },
                    { 10, 3, 23000m, 7 },
                    { 11, 3, 46000m, 1 },
                    { 12, 3, 15000m, 15 },
                    { 1, 4, 12000m, 3 },
                    { 2, 4, 11000m, 7 },
                    { 3, 4, 15000m, 31 },
                    { 13, 4, 20000m, 4 },
                    { 14, 4, 33000m, 7 },
                    { 15, 4, 20000m, 31 },
                    { 1, 5, 8000m, 15 },
                    { 2, 5, 32000m, 25 },
                    { 3, 5, 11000m, 16 },
                    { 13, 5, 18000m, 20 },
                    { 14, 5, 44000m, 25 },
                    { 15, 5, 49000m, 16 },
                    { 7, 6, 16000m, 6 },
                    { 8, 6, 11000m, 18 },
                    { 9, 6, 13000m, 5 },
                    { 10, 6, 23000m, 20 },
                    { 11, 6, 35000m, 25 },
                    { 12, 6, 29000m, 16 },
                    { 7, 7, 18000m, 12 },
                    { 8, 7, 34000m, 11 },
                    { 9, 7, 11000m, 2 },
                    { 7, 8, 13000m, 4 },
                    { 8, 8, 11000m, 7 },
                    { 9, 8, 15000m, 31 },
                    { 7, 9, 9000m, 20 },
                    { 8, 9, 18000m, 25 },
                    { 9, 9, 11000m, 16 },
                    { 10, 10, 46000m, 6 },
                    { 11, 10, 53000m, 18 },
                    { 12, 10, 14300m, 5 },
                    { 13, 10, 17000m, 7 },
                    { 14, 10, 55000m, 1 },
                    { 15, 10, 44000m, 15 },
                    { 4, 11, 11000m, 7 },
                    { 5, 11, 12000m, 1 },
                    { 6, 11, 20000m, 15 },
                    { 10, 11, 60000m, 12 },
                    { 11, 11, 62000m, 11 },
                    { 12, 11, 420000m, 2 },
                    { 4, 12, 16000m, 6 },
                    { 5, 12, 11000m, 18 },
                    { 6, 12, 13000m, 5 },
                    { 4, 13, 32000m, 12 },
                    { 5, 13, 33000m, 11 },
                    { 6, 13, 11000m, 2 },
                    { 4, 14, 13000m, 4 },
                    { 5, 14, 11000m, 7 },
                    { 6, 14, 15000m, 31 },
                    { 13, 14, 31000m, 12 },
                    { 14, 14, 51000m, 11 },
                    { 15, 14, 38000m, 2 },
                    { 4, 15, 9000m, 20 },
                    { 5, 15, 18000m, 25 },
                    { 6, 15, 11000m, 16 },
                    { 15, 21, 56000m, 20 },
                    { 15, 22, 37000m, 22 },
                    { 15, 23, 22000m, 24 },
                    { 15, 24, 54000m, 26 },
                    { 15, 25, 40000m, 28 },
                    { 15, 26, 16000m, 30 },
                    { 15, 27, 62000m, 32 },
                    { 15, 28, 20000m, 34 },
                    { 15, 29, 45000m, 36 },
                    { 15, 210, 43000m, 38 },
                    { 15, 211, 15000m, 40 },
                    { 15, 212, 30000m, 42 },
                    { 15, 213, 24000m, 44 },
                    { 15, 214, 51000m, 46 },
                    { 15, 215, 35000m, 48 },
                    { 15, 216, 34000m, 50 },
                    { 15, 217, 35000m, 52 },
                    { 15, 218, 28000m, 54 },
                    { 15, 219, 20000m, 56 },
                    { 15, 220, 15000m, 58 },
                    { 15, 221, 59000m, 60 },
                    { 15, 222, 23000m, 62 },
                    { 15, 223, 28000m, 64 },
                    { 15, 224, 28000m, 66 },
                    { 15, 225, 32000m, 68 },
                    { 15, 226, 45000m, 70 },
                    { 15, 227, 22000m, 72 },
                    { 15, 228, 40000m, 74 },
                    { 15, 229, 41000m, 76 },
                    { 15, 230, 16000m, 78 },
                    { 15, 231, 52000m, 80 },
                    { 15, 232, 23000m, 82 },
                    { 15, 233, 54000m, 84 },
                    { 15, 234, 49000m, 86 },
                    { 15, 235, 38000m, 88 },
                    { 15, 236, 24000m, 90 },
                    { 15, 237, 65000m, 92 },
                    { 15, 238, 64000m, 94 },
                    { 15, 239, 40000m, 96 },
                    { 15, 240, 22000m, 98 },
                    { 15, 241, 19000m, 100 },
                    { 15, 242, 50000m, 102 },
                    { 15, 243, 31000m, 104 },
                    { 15, 244, 48000m, 106 }
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
