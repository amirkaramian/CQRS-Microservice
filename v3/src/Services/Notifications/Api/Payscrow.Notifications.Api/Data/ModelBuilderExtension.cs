using Microsoft.EntityFrameworkCore;
using Payscrow.Notifications.Api.Application.Enumerations;
using Payscrow.Notifications.Api.Application.Models;
using System;
using System.Collections.Generic;

namespace Payscrow.Notifications.Api.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasData(Tenants());
            modelBuilder.Entity<EmailProvider>().HasData(EmailProviders());
            modelBuilder.Entity<EmailTemplate>().HasData(EmailTemplates());
        }

        public static List<Tenant> Tenants()
        {
            return new List<Tenant> {
                new Tenant
                {
                    Id = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    Name = "PayScrow",
                    EmailAddress = "hello@payscrow.net"
                }
            };
        }

        public static List<EmailProvider> EmailProviders()
        {
            return new List<EmailProvider>
            {
                new EmailProvider
                {
                    Id = Guid.Parse("80ffa783-157d-46cf-9905-ccb22a72d937"),
                    Type = EmailProviderType.SendGrid,
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"),
                    ProviderApiKey = "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",
                    CreateUtc = new DateTime(2022,03,20)
                }
            };
        }

        public static List<EmailTemplate> EmailTemplates()
        {
            return new List<EmailTemplate>
            {
                new EmailTemplate
                {
                    Id = Guid.Parse("efd4e7ef-fd43-4161-8943-786f94b42d36"),
                    Content = "",
                    EmailProviderId = Guid.Parse("80ffa783-157d-46cf-9905-ccb22a72d937"),
                    CreateUtc = new DateTime(2022,03,20),
                    MessageType = EmailMessageType.MarketPlaceEscrowCode,
                    ProviderTemplateId = "d-61c8257fa427411cb5bf71777201b14b",
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
                new EmailTemplate
                {
                    Id = Guid.Parse("259e5fe4-5d76-44b7-9dda-2945ceef7613"),
                    Content = "",
                    EmailProviderId = Guid.Parse("80ffa783-157d-46cf-9905-ccb22a72d937"),
                    CreateUtc = new DateTime(2022,03,20),
                    MessageType = EmailMessageType.MarketPlaceMerchantPaymentNotification,
                    ProviderTemplateId = "d-19e87ca99cff4c7f96203e2a8a41cc39",
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
                new EmailTemplate
                {
                    Id = Guid.Parse("0791209d-2815-407d-a20a-4cb0ecb7051b"),
                    Content = "",
                    EmailProviderId = Guid.Parse("80ffa783-157d-46cf-9905-ccb22a72d937"),
                    CreateUtc = new DateTime(2022,03,20),
                    MessageType = EmailMessageType.NewRegisteredUser,
                    ProviderTemplateId = "d-909fb1fdd5ad48f4b42175208e38cd8b",
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                },
                new EmailTemplate
                {
                    Id = Guid.Parse("87b8343e-9c1d-4ced-b777-255cd5da1c8b"),
                    Content = "",
                    EmailProviderId = Guid.Parse("80ffa783-157d-46cf-9905-ccb22a72d937"),
                    CreateUtc = new DateTime(2022,03,20),
                    MessageType = EmailMessageType.SystemGeneratedUser,
                    ProviderTemplateId = "d-3ff07847949d4a5eb00a349fd0861598",
                    TenantId = Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899")
                }
            };
        }
    }
}