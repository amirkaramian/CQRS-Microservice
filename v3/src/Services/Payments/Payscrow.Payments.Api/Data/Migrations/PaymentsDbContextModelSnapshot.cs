﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payscrow.Payments.Api.Data;

namespace Payscrow.Payments.Api.Data.Migrations
{
    [DbContext(typeof(PaymentsDbContext))]
    partial class PaymentsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.AccountPaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("PaymentMethodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("AccountPaymentMethods");
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            Code = "NGN",
                            CreateUtc = new DateTime(2021, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Naira",
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                            UpdateUtc = new DateTime(2021, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalTransactionRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PaymentMethodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("TransactionGuid");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LogoFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Provider")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            CreateUtc = new DateTime(2021, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            LogoFileName = "flutterwave-logo.png",
                            Name = "Flutterwave",
                            Provider = 1,
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                            UpdateUtc = new DateTime(2021, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("58015b76-1512-492e-8c33-d54f888b7868"),
                            CreateUtc = new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            LogoFileName = "interswitch-logo.png",
                            Name = "Interswitch",
                            Provider = 3,
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                            UpdateUtc = new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.PaymentMethodCurrency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaymentMethodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("PaymentMethodCurrencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3905a4e5-1ddc-4e12-ac3f-6aa59c0ee828"),
                            CreateUtc = new DateTime(2021, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrencyId = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            PaymentMethodId = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                            UpdateUtc = new DateTime(2021, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("5f3548b2-2534-4bdf-9d48-7c672085aca6"),
                            CreateUtc = new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrencyId = new Guid("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                            PaymentMethodId = new Guid("58015b76-1512-492e-8c33-d54f888b7868"),
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                            UpdateUtc = new DateTime(2022, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.Settlement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrencyCode")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("GatewayReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Provider")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TransactionGuid");

                    b.ToTable("Settlements");
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.SettlementAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BankCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("GatewayReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SettlementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SettlementId");

                    b.ToTable("SettlementAccounts");
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.AccountPaymentMethod", b =>
                {
                    b.HasOne("Payscrow.Payments.Api.Domain.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.Payment", b =>
                {
                    b.HasOne("Payscrow.Payments.Api.Domain.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payscrow.Payments.Api.Domain.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId");
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.PaymentMethodCurrency", b =>
                {
                    b.HasOne("Payscrow.Payments.Api.Domain.Models.Currency", "Currency")
                        .WithMany("CurrencyPaymentMethods")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payscrow.Payments.Api.Domain.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("PaymentMethodCurrencies")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Payscrow.Payments.Api.Domain.Models.SettlementAccount", b =>
                {
                    b.HasOne("Payscrow.Payments.Api.Domain.Models.Settlement", "Settlement")
                        .WithMany("SettlementAccounts")
                        .HasForeignKey("SettlementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
