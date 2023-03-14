using AutoMapper;
using MediatR;
using Payscrow.Escrow.Application.Common.Mappings;
using Payscrow.Escrow.Application.Services;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Queries.Business
{
    public static class GetCurrencies
    {
        public class Query : BaseQuery<List<CurrencyDto>>
        {
            private class Handler : IRequestHandler<Query, List<CurrencyDto>>
            {
                private readonly IMapper _mapper;
                private readonly CurrencyService _currencyService;

                public Handler(IMapper mapper, CurrencyService currencyService)
                {
                    _mapper = mapper;
                    _currencyService = currencyService;
                }

                public async Task<List<CurrencyDto>> Handle(Query request, CancellationToken cancellationToken)
                {
                    //var result = new QueryResult();

                    var currencies = await _currencyService.GetAllCurrenciesAsync(request.TenantId);

                    return _mapper.Map<List<Currency>, List<CurrencyDto>>(currencies);

                    //result.Currencies.AddRange(currencyModels);

                    //return result;
                }
            }
        }

        //public class QueryResult
        //{
        //    public List<CurrencyModel> Currencies { get; set; } = new List<CurrencyModel>();
        //}

        public class CurrencyDto : IMapFrom<Currency>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string Code { get; set; }
            public bool IsDefault { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<Currency, CurrencyDto>();
            }
        }
    }
}