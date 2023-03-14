using Payscrow.Payments.Api.Domain.Models;

namespace Payscrow.Payments.Api.Application.Extensions
{
    public static class PaymentMethodExtensions
    {
        public static void SetLogoUri(this PaymentMethod paymentMethod, string baseUrl)
        {
            paymentMethod.LogoUri = $"{baseUrl}{paymentMethod.LogoFileName}";
        }
    }
}
