using Microsoft.EntityFrameworkCore;
using Payscrow.Identity.Api.Models;
using System;
using System.Collections.Generic;

namespace Payscrow.Identity.Api.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasData(Tenants());
            modelBuilder.Entity<Host>().HasData(Hosts());
        }

        public static List<Tenant> Tenants()
        {
            return new List<Tenant> {
                new Tenant
                {
                    Id = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    Colour = "Green",
                    CompanyName = "Payscrow",
                    ContactEmail = "hello@payscrow.net",
                    ContactPhone = "08037452476"
                }
            };
        }

        public static List<Host> Hosts()
        {
            return new List<Host>
            {
                new Host
                {
                    Id = Guid.Parse("81ae491e-7c4e-4f8d-93cb-adc8016355c2"),
                    Name = "host.docker.internal:7100",
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
                new Host
                {
                    Id = Guid.Parse("009602d0-7b36-404e-9496-e5946a3f2633"),
                    Name = "host.docker.internal:8000",
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
                 new Host
                {
                    Id = Guid.Parse("e24f5a65-c452-47f7-8f37-b1c24dc3f6aa"),
                    Name = "payscrow.net",
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                }
            };
        }
    }
}