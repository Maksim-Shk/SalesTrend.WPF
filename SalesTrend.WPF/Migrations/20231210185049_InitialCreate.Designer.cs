﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SalesTrend.WPF.Models;

#nullable disable

namespace SalesTrend.WPF.Migrations
{
    [DbContext(typeof(SalesTrendContext))]
    [Migration("20231210185049_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SalesTrend.WPF.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddressId"));

                    b.Property<string>("Apartment")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("House")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer");

                    b.Property<int>("StreetId")
                        .HasColumnType("integer");

                    b.HasKey("AddressId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("LocalityId");

                    b.HasIndex("StreetId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.ClientOrder", b =>
                {
                    b.Property<int>("ClientOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClientOrderId"));

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ClientOrderId");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientOrder");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.ClientOrderProduct", b =>
                {
                    b.Property<int>("ClientOrderProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClientOrderProductId"));

                    b.Property<int>("ClientOrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ClientOrderProductId");

                    b.HasIndex("ClientOrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ClientOrderProduct");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContactPersonFullName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Url")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Individual", b =>
                {
                    b.Property<Guid>("IndividualId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IssuedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("PassportNumber")
                        .HasColumnType("integer");

                    b.Property<int>("PassportSerial")
                        .HasColumnType("integer");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("TaxNumber")
                        .HasColumnType("integer");

                    b.HasKey("IndividualId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Individual");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.LegalEntity", b =>
                {
                    b.Property<Guid>("LegalEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContactPersonFullName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("TaxNumber")
                        .HasColumnType("integer");

                    b.HasKey("LegalEntityId");

                    b.ToTable("LegalEntity");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Locality", b =>
                {
                    b.Property<int>("LocalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LocalityId"));

                    b.Property<int>("LocalityTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("LocalityId");

                    b.HasIndex("LocalityTypeId");

                    b.ToTable("Locality");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.LocalityType", b =>
                {
                    b.Property<int>("LocalityTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LocalityTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("LocalityTypeId");

                    b.ToTable("LocalityType");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Phone", b =>
                {
                    b.Property<int>("PhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PhoneId"));

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PhoneId");

                    b.HasIndex("EntityId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.PriceList", b =>
                {
                    b.Property<int>("PriceListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PriceListId"));

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PriceListId");

                    b.HasIndex("CompanyId");

                    b.ToTable("PriceList");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.PriceListProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("PriceListId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ProductId", "PriceListId");

                    b.HasIndex("PriceListId");

                    b.ToTable("PriceListProduct");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Article")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Street", b =>
                {
                    b.Property<int>("StreetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StreetId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("StreeTypetId")
                        .HasColumnType("integer");

                    b.HasKey("StreetId");

                    b.HasIndex("StreeTypetId");

                    b.ToTable("Street");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.StreetType", b =>
                {
                    b.Property<int>("StreetTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StreetTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("StreetTypeId");

                    b.ToTable("StreetType");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Address", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.Company", "Company")
                        .WithMany("Addresses")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesTrend.WPF.Models.LegalEntity", "LegalEntity")
                        .WithMany("Addresses")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesTrend.WPF.Models.Locality", "Locality")
                        .WithMany("Addresses")
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesTrend.WPF.Models.Street", "Street")
                        .WithMany("Addresses")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("LegalEntity");

                    b.Navigation("Locality");

                    b.Navigation("Street");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.ClientOrder", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.Individual", "Individual")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesTrend.WPF.Models.LegalEntity", "LegalEntity")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Individual");

                    b.Navigation("LegalEntity");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.ClientOrderProduct", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.ClientOrder", "ClientOrder")
                        .WithMany("ClientOrderProducts")
                        .HasForeignKey("ClientOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesTrend.WPF.Models.Product", "Product")
                        .WithMany("ClientOrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientOrder");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Individual", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.Address", "Address")
                        .WithOne("Individual")
                        .HasForeignKey("SalesTrend.WPF.Models.Individual", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Locality", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.LocalityType", "LocalityType")
                        .WithMany("Localities")
                        .HasForeignKey("LocalityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocalityType");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Phone", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.Company", "Company")
                        .WithMany("Phones")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesTrend.WPF.Models.LegalEntity", "LegalEntity")
                        .WithMany("Phones")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("LegalEntity");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.PriceList", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.Company", "Company")
                        .WithMany("PriceLists")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.PriceListProduct", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.PriceList", "PriceList")
                        .WithMany("PriceListProducts")
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesTrend.WPF.Models.Product", "Product")
                        .WithMany("PriceListProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceList");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Street", b =>
                {
                    b.HasOne("SalesTrend.WPF.Models.StreetType", "StreetType")
                        .WithMany()
                        .HasForeignKey("StreeTypetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StreetType");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Address", b =>
                {
                    b.Navigation("Individual");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.ClientOrder", b =>
                {
                    b.Navigation("ClientOrderProducts");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Company", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Phones");

                    b.Navigation("PriceLists");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.LegalEntity", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Phones");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Locality", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.LocalityType", b =>
                {
                    b.Navigation("Localities");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.PriceList", b =>
                {
                    b.Navigation("PriceListProducts");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Product", b =>
                {
                    b.Navigation("ClientOrderProducts");

                    b.Navigation("PriceListProducts");
                });

            modelBuilder.Entity("SalesTrend.WPF.Models.Street", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
