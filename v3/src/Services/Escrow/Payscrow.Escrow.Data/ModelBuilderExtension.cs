using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Collections.Generic;

namespace Payscrow.Escrow.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(Currencies());
        }

        public static List<Currency> Currencies()
        {
            return new List<Currency> {
                new Currency
                {
                    Id = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    Name = "Naira",
                    Code = "NGN",
                    Symbol = "N",
                    IsActive = true,
                    IsDefault = true,
                    CreateUtc = new DateTime(2020,12,24),
                    UpdateUtc = new DateTime(2020,12,24),
                    Order = 1
                },
               new Currency
                {
                    Id = Guid.Parse("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    Name = "US Dollar",
                    Code = "USD",
                    Symbol = "$",
                    IsActive = false,
                    IsDefault = false,
                    CreateUtc = new DateTime(2020,12,24),
                    UpdateUtc = new DateTime(2020,12,24),
                    Order = 2
                },
               new Currency
                {
                    Id = Guid.Parse("9824e994-7b7d-439a-8322-a07a4b64165d"),
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    Name = "franc",
                    Code = "CFA",
                    Symbol = "C",
                    IsActive = false,
                    IsDefault = false,
                    CreateUtc = new DateTime(2020,12,24),
                    UpdateUtc = new DateTime(2020,12,24),
                    Order = 2
                },
            };
        }
    }
}