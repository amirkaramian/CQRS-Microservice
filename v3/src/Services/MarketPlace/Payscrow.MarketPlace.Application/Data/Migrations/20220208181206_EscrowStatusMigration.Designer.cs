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
    [Migration("20220208181206_EscrowStatusMigration")]
    partial class EscrowStatusMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.BrokerConfig", b =>
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

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FixedRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BrokerConfigs");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.ChargeConfig", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
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
                            Id = new Guid("ee053a50-43d6-4e9e-acdf-ae36013c6f4f"),
                            CurrencyId = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            FixedRate = 200m,
                            MaxTransactionAmount = 100000m,
                            MinTransactionAmount = 0m,
                            Percentage = 1m,
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        },
                        new
                        {
                            Id = new Guid("b10e53b5-e6bb-49ac-b79c-ae36013c6f4f"),
                            CurrencyId = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            FixedRate = 2000m,
                            MaxTransactionAmount = 792281625142643m,
                            MinTransactionAmount = 100000m,
                            Percentage = 0.5m,
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        },
                        new
                        {
                            Id = new Guid("dcdbeed7-3526-4a20-ba7e-ae36013c6f4f"),
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

                    b.HasIndex("TenantId");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
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

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Name")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.SettlementAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AccountNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BankCode")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("SettlementAccounts");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BrokerAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BrokerFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BrokerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrokerTransactionReference")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CustomerCharge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerEmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EscrowCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GrandTotalPayable")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsEscrow")
                        .HasColumnType("bit");

                    b.Property<Guid>("MerchantAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("MerchantCharge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MerchantEmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MerchantName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BrokerTransactionReference");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("Number")
                        .IsUnique()
                        .HasFilter("[Number] IS NOT NULL");

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

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Item", b =>
                {
                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", "Transaction")
                        .WithMany("Items")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
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

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.SettlementAccount", b =>
                {
                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", "Transaction")
                        .WithMany("SettlementAccounts")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Payscrow.MarketPlace.Application.Domain.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("Payscrow.MarketPlace.Application.Domain.Entities.Transaction", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("SettlementAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
