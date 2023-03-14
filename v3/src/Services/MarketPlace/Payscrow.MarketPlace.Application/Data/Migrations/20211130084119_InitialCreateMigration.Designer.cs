﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payscrow.MarketPlace.Application.Data;

namespace Payscrow.MarketPlace.Application.Data.Migrations
{
    [DbContext(typeof(MarketPlaceDbContext))]
    [Migration("20211130084119_InitialCreateMigration")]
    partial class InitialCreateMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Broker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Brokers");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.ChargeConfig", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("FixedRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxTransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinTransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("ChargeConfigs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12be2f05-ef81-443a-9539-adf0009fa8a1"),
                            AccountId = new Guid("00000000-0000-0000-0000-000000000000"),
                            CurrencyId = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            FixedRate = 200m,
                            MaxTransactionAmount = 100000m,
                            MinTransactionAmount = 0m,
                            Percentage = 1m,
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        },
                        new
                        {
                            Id = new Guid("6b4d3838-c904-4637-aa84-adf0009fa8a1"),
                            AccountId = new Guid("00000000-0000-0000-0000-000000000000"),
                            CurrencyId = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            FixedRate = 2000m,
                            MaxTransactionAmount = 792281625142643m,
                            MinTransactionAmount = 100000m,
                            Percentage = 0.5m,
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        },
                        new
                        {
                            Id = new Guid("825fb602-365e-4054-a4bb-adf0009fa8a1"),
                            AccountId = new Guid("00000000-0000-0000-0000-000000000000"),
                            CurrencyId = new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                            FixedRate = 2000m,
                            MaxTransactionAmount = 79228162514264m,
                            MinTransactionAmount = 0m,
                            Percentage = 0.5m,
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        });
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ChargeCap")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ChargeMethod")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            AccountId = new Guid("00000000-0000-0000-0000-000000000000"),
                            ChargeCap = 2000m,
                            ChargeMethod = 15,
                            Code = "NGN",
                            IsActive = true,
                            Name = "Naira",
                            Order = 1,
                            Symbol = "N",
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        },
                        new
                        {
                            Id = new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                            AccountId = new Guid("00000000-0000-0000-0000-000000000000"),
                            ChargeCap = 100m,
                            ChargeMethod = 5,
                            Code = "USD",
                            IsActive = false,
                            Name = "US Dollar",
                            Order = 2,
                            Symbol = "$",
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        });
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Merchant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.PaymentAttemptLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("PaymentAttemptLogs");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BrokerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MerchantId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.ChargeConfig", b =>
                {
                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Payment", b =>
                {
                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.PaymentAttemptLog", b =>
                {
                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Broker", "Broker")
                        .WithMany()
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Merchant", "Merchant")
                        .WithMany()
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Broker");

                    b.Navigation("Customer");

                    b.Navigation("Merchant");
                });
#pragma warning restore 612, 618
        }
    }
}