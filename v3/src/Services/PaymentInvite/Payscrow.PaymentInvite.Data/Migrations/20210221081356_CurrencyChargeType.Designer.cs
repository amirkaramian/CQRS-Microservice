﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payscrow.PaymentInvite.Data;

namespace Payscrow.PaymentInvite.Data.Migrations
{
    [DbContext(typeof(PaymentInviteDbContext))]
    [Migration("20210221081356_CurrencyChargeType")]
    partial class CurrencyChargeType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ChargeType")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FixedCharge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<decimal>("PercentageCharge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            ChargeType = 3,
                            Code = "NGN",
                            CreateUtc = new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FixedCharge = 100m,
                            IsActive = true,
                            Name = "Naira",
                            Order = 1,
                            PercentageCharge = 1.5m,
                            Symbol = "N",
                            UpdateUtc = new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                            ChargeType = 2,
                            Code = "USD",
                            CreateUtc = new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FixedCharge = 5m,
                            IsActive = false,
                            Name = "US Dollar",
                            Order = 2,
                            PercentageCharge = 1m,
                            Symbol = "$",
                            UpdateUtc = new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Deal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuyerLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<decimal>("SellerChargePercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SellerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SellerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SellerVerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("SellerId");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.DealItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DealId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DealId");

                    b.ToTable("DealItems");
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Trader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdentityGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdentityGuid");

                    b.ToTable("Traders");
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BuyerChargeAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BuyerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DealId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DealId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("InEscrow")
                        .HasColumnType("bit");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("SellerChargeAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("TraderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BuyerEmail");

                    b.HasIndex("BuyerId");

                    b.HasIndex("DealId");

                    b.HasIndex("DealId1");

                    b.HasIndex("TraderId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.TransactionItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DealItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DealItemId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionItems");
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Deal", b =>
                {
                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Trader", "Seller")
                        .WithMany("Deals")
                        .HasForeignKey("SellerId");

                    b.OwnsOne("Payscrow.PaymentInvite.Domain.ValueObjects.PhoneNumber", "SellerPhone", b1 =>
                        {
                            b1.Property<Guid>("DealId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CountryCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LocalNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("DealId");

                            b1.ToTable("Deals");

                            b1.WithOwner()
                                .HasForeignKey("DealId");
                        });
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.DealItem", b =>
                {
                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Deal", "Deal")
                        .WithMany("Items")
                        .HasForeignKey("DealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Note", b =>
                {
                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Transaction", "Transaction")
                        .WithMany("Notes")
                        .HasForeignKey("TransactionId");
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Trader", b =>
                {
                    b.OwnsOne("Payscrow.PaymentInvite.Domain.ValueObjects.Address", "ContactAddress", b1 =>
                        {
                            b1.Property<Guid>("TraderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TraderId");

                            b1.ToTable("Traders");

                            b1.WithOwner()
                                .HasForeignKey("TraderId");
                        });

                    b.OwnsOne("Payscrow.PaymentInvite.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("TraderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CountryCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LocalNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TraderId");

                            b1.ToTable("Traders");

                            b1.WithOwner()
                                .HasForeignKey("TraderId");
                        });
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.Transaction", b =>
                {
                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Trader", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Deal", "Deal")
                        .WithMany()
                        .HasForeignKey("DealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Deal", null)
                        .WithMany("Transactions")
                        .HasForeignKey("DealId1");

                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Trader", null)
                        .WithMany("Transactions")
                        .HasForeignKey("TraderId");

                    b.OwnsOne("Payscrow.PaymentInvite.Domain.ValueObjects.PhoneNumber", "BuyerPhone", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CountryCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LocalNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });
                });

            modelBuilder.Entity("Payscrow.PaymentInvite.Domain.Models.TransactionItem", b =>
                {
                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.DealItem", "DealItem")
                        .WithMany()
                        .HasForeignKey("DealItemId");

                    b.HasOne("Payscrow.PaymentInvite.Domain.Models.Transaction", "Transaction")
                        .WithMany("Items")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
