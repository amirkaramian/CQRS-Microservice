using Microsoft.EntityFrameworkCore;
using Payscrow.EscrowDirect.Application.Domain.Entities;
using Payscrow.EscrowDirect.Application.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace Payscrow.EscrowDirect.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(Currencies());
            modelBuilder.Entity<ChargeConfig>().HasData(ChargeConfigurations());
        }

        public static List<Currency> Currencies()
        {
            return new List<Currency> {
                new Currency
                {
                    Id = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    Name = "Naira",
                    Code = "NGN",
                    Symbol = "N",
                    IsActive = true,
                    Order = 1,
                    ChargeMethod = ChargeMethod.Combination,
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    ChargeCap = 2000
                },
               new Currency
                {
                    Id = Guid.Parse("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    Name = "US Dollar",
                    Code = "USD",
                    Symbol = "$",
                    IsActive = false,
                    Order = 2,
                    ChargeCap = 100,
                    ChargeMethod = ChargeMethod.Percentage
                }
            };
        }

        public static List<ChargeConfig> ChargeConfigurations()
        {
            return new List<ChargeConfig>
            {
                // naira configurations
                new ChargeConfig
                {
                    CurrencyId = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    MaxTransactionAmount = 100000,
                    MinTransactionAmount = 0,
                    Percentage = 1,
                    FixedRate = 200,
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
                new ChargeConfig
                {
                    CurrencyId = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    MaxTransactionAmount = 792281625142643m,
                    MinTransactionAmount = 100000,
                    Percentage = 0.5m,
                    FixedRate = 2000,
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
                // dollar configurations
                 new ChargeConfig
                {
                    CurrencyId = Guid.Parse("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                    MaxTransactionAmount = 79228162514264m,
                    MinTransactionAmount = 0,
                    Percentage = 0.5m,
                    FixedRate = 2000,
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
            };
        }
    }
}
