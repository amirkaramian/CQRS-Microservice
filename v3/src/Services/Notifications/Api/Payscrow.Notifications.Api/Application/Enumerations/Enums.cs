namespace Payscrow.Notifications.Api.Application.Enumerations
{
    public enum EmailMessageType
    {
        PaymentInvite = 10,
        GuestDeal = 20,
        DealCreatedAndVerified = 30,
        ProjectInvite = 40,

        // Market-Place Types
        MarketPlaceEscrowCode = 60,

        MarketPlaceMerchantPaymentNotification = 70,

        // Identity
        EmailVerificationCode = 50,

        NewRegisteredUser = 200,

        SystemGeneratedUser = 210
    }

    public enum EmailProviderType
    {
        SendGrid
    }
}