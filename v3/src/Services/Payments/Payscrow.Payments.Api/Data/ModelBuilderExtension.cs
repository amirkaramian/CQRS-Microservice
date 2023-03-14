using Microsoft.EntityFrameworkCore;
using Payscrow.Payments.Api.Domain.Enumerations;
using Payscrow.Payments.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace Payscrow.Payments.Api.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethod>().HasData(PaymentMethods());
            modelBuilder.Entity<Currency>().HasData(Currencies());
            modelBuilder.Entity<PaymentMethodCurrency>().HasData(PaymentMethodCurrencies());
        }

        public static List<Currency> Currencies()
        {
            return new List<Currency> {
                new Currency
                {
                    Id = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    Code = "NGN",
                    CreateUtc = new DateTime(2021, 11, 17),
                    UpdateUtc = new DateTime(2021, 11, 17),
                    Name = "Naira",
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                }
            };
        }

        public static List<PaymentMethod> PaymentMethods()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod {
                    Id = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    Name = "Flutterwave",
                    CreateUtc = new DateTime(2021,2,21),
                    UpdateUtc = new DateTime(2021,2,21),
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    Provider = PaymentMethodProvider.Flutterwave,
                    IsActive = true,
                    LogoFileName = "flutterwave-logo.png"
                },
                new PaymentMethod
                {
                    Id = Guid.Parse("58015b76-1512-492e-8c33-d54f888b7868"),
                    Name = "Interswitch",
                    CreateUtc = new DateTime(2022,3,17),
                    UpdateUtc = new DateTime(2022,3,17),
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    Provider = PaymentMethodProvider.Interswitch,
                    IsActive = true,
                    LogoFileName = "interswitch-logo.png"
                }
            };
        }

        public static List<PaymentMethodCurrency> PaymentMethodCurrencies()
        {
            return new List<PaymentMethodCurrency>
            {
                new PaymentMethodCurrency
                {
                    Id = Guid.Parse("3905a4e5-1ddc-4e12-ac3f-6aa59c0ee828"),
                    CurrencyId = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    PaymentMethodId = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    CreateUtc = new DateTime(2021,2,21),
                    UpdateUtc = new DateTime(2021,2,21),
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
                 new PaymentMethodCurrency
                {
                    Id = Guid.Parse("5f3548b2-2534-4bdf-9d48-7c672085aca6"),
                    CurrencyId = Guid.Parse("0887bc80-ca1b-4629-a476-b51083a6c09c"),
                    PaymentMethodId = Guid.Parse("58015b76-1512-492e-8c33-d54f888b7868"),
                    CreateUtc = new DateTime(2022,3,17),
                    UpdateUtc = new DateTime(2022,3,17),
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                }
            };
        }
    }
}