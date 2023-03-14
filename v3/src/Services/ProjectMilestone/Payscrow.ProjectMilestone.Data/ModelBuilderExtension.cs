using Microsoft.EntityFrameworkCore;
using Payscrow.ProjectMilestone.Application.Domain.Models;
using System;
using System.Collections.Generic;

namespace Payscrow.ProjectMilestone.Data
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
                    Name = "Naira",
                    Code = "NGN",
                    Symbol = "N",
                    IsActive = true,
                    CreateUtc = new DateTime(2020,12,24),
                    UpdateUtc = new DateTime(2020,12,24),
                    Order = 1
                },
               new Currency
                {
                    Id = Guid.Parse("1c9695e0-330b-4b5b-a866-edd0efe674fa"),
                    Name = "US Dollar",
                    Code = "USD",
                    Symbol = "$",
                    IsActive = false,
                    CreateUtc = new DateTime(2020,12,24),
                    UpdateUtc = new DateTime(2020,12,24),
                    Order = 2
                }
            };
        }
    }
}
