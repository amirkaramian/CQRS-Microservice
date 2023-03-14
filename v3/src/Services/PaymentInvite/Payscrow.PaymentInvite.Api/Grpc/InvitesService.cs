using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Payscrow.PaymentInvite.Application.Commands;
using Payscrow.PaymentInvite.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Api.Grpc
{
    public class InvitesService : PaymentInviteGrpc.PaymentInviteGrpcBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InvitesService> _logger;

        public InvitesService(IMediator mediator, ILogger<InvitesService> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        //public override async Task<CreateInviteCommandResult> createPaymentInvite(CreateInviteCommand createInviteCommand, ServerCallContext context)
        //{
        //    _logger.LogInformation("Begin grpc call from method {Method} for ordering get order draft {createInviteCommand}", context.Method, createInviteCommand);

        //    var command = new CreatePaymentInvite.Command
        //    {
        //        BuyerEmail = createInviteCommand.BuyerEmail,
        //        BuyerCountryCode = createInviteCommand.BuyerCountryCode,
        //        BuyerLocalPhoneNumber = createInviteCommand.BuyerLocalPhoneNumber,
        //        SellerEmail = createInviteCommand.SellerEmail,
        //        SellerCountryCode = createInviteCommand.SellerCountryCode,
        //        SellerLocalPhoneNumber = createInviteCommand.SellerLocalPhoneNumber,
        //        SellerChargePercentage = (decimal)createInviteCommand.SellerChargePercentage,
        //        CurrencyCode = createInviteCommand.CurrencyCode,
        //        Items = MapTradeItems(createInviteCommand.Items)
        //    };

        //    var result =  await _mediator.Send(command);

        //    return new CreateInviteCommandResult { InviteId = result.InviteId.ToString() };
        //}


        //private List<CreatePaymentInvite.Command.TradeItemModel> MapTradeItems(IEnumerable<TradeItemDto> items)
        //{
        //    return items?.Select(x => new CreatePaymentInvite.Command.TradeItemModel {
        //        Amount = (decimal)x.Amount,
        //        Quantity = x.Quantity,
        //        Description = x.Description
        //    }).ToList();
        //}
    }
}
