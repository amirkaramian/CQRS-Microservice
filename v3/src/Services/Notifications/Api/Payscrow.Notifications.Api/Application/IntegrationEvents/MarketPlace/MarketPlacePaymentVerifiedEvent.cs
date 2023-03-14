using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;
using Payscrow.Core.Events;
using Payscrow.Notifications.Api.Application.Enumerations;
using Payscrow.Notifications.Api.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Application.IntegrationEvents.MarketPlace
{
    public class MarketPlacePaymentVerifiedEvent : IntegrationEvent
    {
        public MarketPlacePaymentVerifiedEvent(Guid tenantId, Guid transactionGuid, string transactionNumber, string escrowCode, Guid customerAccountId, string customerEmailAddress,
            string customerPhone, Guid merchantAccountId, string merchantEmailAddress, string merchantPhone, Guid brokerAccountId, decimal amountPaid, string currencyCode)
            : base(tenantId)
        {
            TransactionGuid = transactionGuid;
            EscrowCode = escrowCode;
            TransactionNumber = transactionNumber;
            CustomerAccountId = customerAccountId;
            CustomerEmailAddress = customerEmailAddress;
            CustomerPhone = customerPhone;
            MerchantAccountId = merchantAccountId;
            MerchantEmailAddress = merchantEmailAddress;
            MerchantPhone = merchantPhone;
            BrokerAccountId = brokerAccountId;
            AmountPaid = amountPaid;
            CurrencyCode = currencyCode;
        }

        public Guid TransactionGuid { get; }
        public string TransactionNumber { get; }
        public string EscrowCode { get; }
        public Guid CustomerAccountId { get; }
        public string CustomerEmailAddress { get; }
        public string CustomerPhone { get; }
        public Guid MerchantAccountId { get; }
        public string MerchantEmailAddress { get; }
        public string MerchantPhone { get; }
        public Guid BrokerAccountId { get; }
        public decimal AmountPaid { get; }
        public string CurrencyCode { get; }
    }

    public class MarketPlacePaymentVerifiedEventHandler : IEventHandler<MarketPlacePaymentVerifiedEvent>, IEventHandlerService
    {
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly ILogger _logger;

        public MarketPlacePaymentVerifiedEventHandler(IEmailNotificationService emailNotificationService, ILogger<MarketPlacePaymentVerifiedEventHandler> logger)
        {
            _emailNotificationService = emailNotificationService;
            _logger = logger;
        }

        public async Task Handle(MarketPlacePaymentVerifiedEvent @event)
        {
            _logger.LogInformation("---- Sending MarketPlace Service payment verified escrow code! Code: {Code} ----", @event.EscrowCode);

            await _emailNotificationService.SendAsync(
                        @event.TenantId,
                       EmailMessageType.MarketPlaceEscrowCode,
                       @event.CustomerEmailAddress,
                       "Payment Escrow Code",
                       new Dictionary<string, object> { { "code", @event.EscrowCode } });

            _logger.LogInformation("---- Sending MarketPlace Service Merchant notifiation after payment verified. paid amount: {Currency}{Amount} ----", @event.CurrencyCode, @event.AmountPaid.ToString("N2"));

            await _emailNotificationService.SendAsync(
                        @event.TenantId,
                        EmailMessageType.MarketPlaceMerchantPaymentNotification,
                        @event.MerchantEmailAddress,
                        "Payment Notification",
                        new Dictionary<string, object> {
                            {"amount", @event.AmountPaid.ToString("N2") },
                            { "transactionNumber", @event.TransactionNumber },
                            {"customerEmail", @event.CustomerEmailAddress },
                            { "currencyCode", @event.CurrencyCode },
                            { "merchantEmail", @event.MerchantEmailAddress }
                        });
        }
    }
}