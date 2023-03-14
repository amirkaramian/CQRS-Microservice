﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payscrow.Notifications.Api.Data;

namespace Payscrow.Notifications.Api.Data.Migrations
{
    [DbContext(typeof(NotificationDbContext))]
    [Migration("20220320103247_EmailProviderMigration")]
    partial class EmailProviderMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payscrow.Notifications.Api.Application.Models.EmailLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailLogs");
                });

            modelBuilder.Entity("Payscrow.Notifications.Api.Application.Models.EmailProvider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProviderApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EmailProviders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"),
                            CreateUtc = new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProviderApiKey = "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",
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                            Type = 0
                        });
                });

            modelBuilder.Entity("Payscrow.Notifications.Api.Application.Models.EmailTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmailProviderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MessageType")
                        .HasColumnType("int");

                    b.Property<string>("ProviderTemplateId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmailProviderId");

                    b.HasIndex("TenantId");

                    b.ToTable("EmailTemplates");

                    b.HasData(
                        new
                        {
                            Id = new Guid("efd4e7ef-fd43-4161-8943-786f94b42d36"),
                            Content = "",
                            CreateUtc = new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailProviderId = new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"),
                            MessageType = 60,
                            ProviderTemplateId = "d-61c8257fa427411cb5bf71777201b14b",
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        },
                        new
                        {
                            Id = new Guid("259e5fe4-5d76-44b7-9dda-2945ceef7613"),
                            Content = "",
                            CreateUtc = new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailProviderId = new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"),
                            MessageType = 70,
                            ProviderTemplateId = "d-19e87ca99cff4c7f96203e2a8a41cc39",
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        },
                        new
                        {
                            Id = new Guid("0791209d-2815-407d-a20a-4cb0ecb7051b"),
                            Content = "",
                            CreateUtc = new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailProviderId = new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"),
                            MessageType = 200,
                            ProviderTemplateId = "d-909fb1fdd5ad48f4b42175208e38cd8b",
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        },
                        new
                        {
                            Id = new Guid("87b8343e-9c1d-4ced-b777-255cd5da1c8b"),
                            Content = "",
                            CreateUtc = new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailProviderId = new Guid("80ffa783-157d-46cf-9905-ccb22a72d937"),
                            MessageType = 210,
                            ProviderTemplateId = "d-3ff07847949d4a5eb00a349fd0861598",
                            TenantId = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899")
                        });
                });

            modelBuilder.Entity("Payscrow.Notifications.Api.Application.Models.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            Id = new Guid("30867e39-acca-4565-b5e5-c3785b6f8899"),
                            EmailAddress = "hello@payscrow.net",
                            Name = "PayScrow"
                        });
                });

            modelBuilder.Entity("Payscrow.Notifications.Api.Application.Models.EmailTemplate", b =>
                {
                    b.HasOne("Payscrow.Notifications.Api.Application.Models.EmailProvider", "EmailProvider")
                        .WithMany("EmailTemplates")
                        .HasForeignKey("EmailProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Payscrow.Notifications.Api.Application.Models.Tenant", null)
                        .WithMany("EmailTemplateReferences")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
