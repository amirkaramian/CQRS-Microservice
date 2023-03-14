using MediatR;
using Payscrow.Escrow.Application.Common.Exceptions;
using Payscrow.Escrow.Application.Services;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Queries.Business
{
    public static class GetAccountSettings
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid AccountGuid { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly AccountSettingService _accountSettingService;
                private readonly CurrencyService _currencyService;

                public Handler(AccountSettingService accountSettingService, CurrencyService currencyService)
                {
                    _accountSettingService = accountSettingService;
                    _currencyService = currencyService;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    var accountSetting = await _accountSettingService.GetAccountSettingAsync(request.AccountGuid, request.TenantId);
                    Currency currency;

                    if (accountSetting == null) throw new NotFoundException(nameof(accountSetting), request.AccountGuid);

                    if (accountSetting.DefaultCurrencyId.HasValue)
                    {
                        currency = await _currencyService.GetCurrencyByIdAsync(accountSetting.DefaultCurrencyId.Value, request.TenantId);
                    }
                    else
                    {
                        currency = (await _currencyService.GetAllCurrenciesAsync(request.TenantId)).OrderBy(x => x.IsDefault).FirstOrDefault();
                    }

                    if (currency == null) throw new NotFoundException(nameof(currency), "default currency not found!");

                    result.AccountSetting.CurrencyCode = currency.Code;
                    result.AccountSetting.CurrencyName = currency.Name;
                    result.AccountSetting.CurrencySymbol = currency.Symbol;

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public AccountSettingDto AccountSetting { get; set; } = new AccountSettingDto();
        }

        public class AccountSettingDto
        {
            public string CurrencyCode { get; set; }
            public string CurrencyName { get; set; }
            public string CurrencySymbol { get; set; }
        }
    }
}