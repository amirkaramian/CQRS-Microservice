using MediatR;
using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Queries.Business
{
    public static class GetAccountStatistics
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid AccountGuid { get; set; }
            public string CurrencyCode { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly IEscrowDbContext _context;

                public Handler(IEscrowDbContext context)
                {
                    _context = context;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    var result = new QueryResult();

                    result.AmountInEscrow = await (from a in _context.Accounts.ForTenant(request.TenantId)
                                                   join eta in _context.EscrowTransactionAccounts.ForTenant(request.TenantId) on a.Id equals eta.AccountId
                                                   join et in _context.EscrowTransactions.ForTenant(request.TenantId) on eta.EscrowTransactionId equals et.Id
                                                   join c in _context.Currencies.ForTenant(request.TenantId) on et.CurrencyId equals c.Id
                                                   where a.AccountGuid == request.AccountGuid && !et.IsReleased && c.Code == request.CurrencyCode
                                                   select et.Amount).SumAsync();

                    result.NumberOfTransactions = await (from a in _context.Accounts.ForTenant(request.TenantId)
                                                         join eta in _context.EscrowTransactionAccounts.ForTenant(request.TenantId) on a.Id equals eta.AccountId
                                                         join et in _context.EscrowTransactions.ForTenant(request.TenantId) on eta.EscrowTransactionId equals et.Id
                                                         join c in _context.Currencies.ForTenant(request.TenantId) on et.CurrencyId equals c.Id
                                                         where a.AccountGuid == request.AccountGuid && !et.IsReleased && c.Code == request.CurrencyCode
                                                         select et).CountAsync();

                    var escrowTransactions = (await (from et in _context.EscrowTransactions.ForTenant(request.TenantId)
                                                     join etaf in _context.EscrowTransactionAccounts.ForTenant(request.TenantId) on et.Id equals etaf.EscrowTransactionId
                                                     join eta in _context.EscrowTransactionAccounts.ForTenant(request.TenantId).Include(x => x.Account) on et.Id equals eta.EscrowTransactionId
                                                     join a in _context.Accounts.ForTenant(request.TenantId) on etaf.AccountId equals a.Id
                                                     join c in _context.Currencies.ForTenant(request.TenantId) on et.CurrencyId equals c.Id
                                                     where a.AccountGuid == request.AccountGuid && c.Code == request.CurrencyCode && et.StatusId != EscrowTransactionStatus.CompletedSettlement.Id
                                                     orderby et.CreateUtc descending
                                                     select new { et, eta })
                                                       .Take(20)
                                                       .ToListAsync())
                                                       .GroupBy(x => x.et.Id, (key, g) => new { Id = key, Trans = g.ToList() });

                    foreach (var t in escrowTransactions)
                    {
                        var dbEt = t.Trans.Select(x => x.et).FirstOrDefault();

                        var et = new EscrowTransactionDto
                        {
                            Id = t.Id,
                            TransactionNumber = dbEt.TransactionNumber,
                            Amount = dbEt.Amount,
                            CreateUtc = dbEt.CreateUtc,
                            TransactionGuid = dbEt.TransactionGuid,
                            StatusId = dbEt.StatusId,
                            IsReleased = dbEt.IsReleased,
                            InDispute = dbEt.InDispute,
                            //ServiceName = Enumeration.FromValue<ServiceType>(dbEt.ServiceTypeId).Name,
                            ServiceTypeId = dbEt.ServiceTypeId,
                            EscrowTransactionAccounts = t.Trans.ConvertAll(x => new EscrowTransactionDto.EscrowTransactionAccountDto
                            {
                                AccountId = x.eta.AccountId,
                                EscrowTransactionRoleId = x.eta.EscrowTransactionRoleId,
                                EscrowTransactionId = x.eta.EscrowTransactionId,
                                AccountName = x.eta.Account.Name
                            })
                        };

                        result.EscrowTransactions.Add(et);
                    }

                    return result;
                }
            }
        }

        public class QueryResult
        {
            public decimal AmountInEscrow { get; set; }
            public int NumberOfTransactions { get; set; }
            public decimal AmountInWallet { get; set; }
            public List<EscrowTransactionDto> EscrowTransactions { get; set; } = new List<EscrowTransactionDto>();
        }

        public class EscrowTransactionDto
        {
            public Guid Id { get; set; }
            public string TransactionNumber { get; set; }
            public decimal Amount { get; set; }
            public DateTime CreateUtc { get; set; }
            public bool IsReleased { get; set; }
            public bool InDispute { get; set; }
            public Guid TransactionGuid { get; set; }
            public int StatusId { get; set; }
            public EscrowTransactionStatus Status => Enumeration.FromValue<EscrowTransactionStatus>(StatusId);

            public int ServiceTypeId { get; set; }
            public ServiceType ServiceType => Enumeration.FromValue<ServiceType>(ServiceTypeId);

            public List<EscrowTransactionAccountDto> EscrowTransactionAccounts { get; set; } = new List<EscrowTransactionAccountDto>();

            public class EscrowTransactionAccountDto
            {
                public int EscrowTransactionRoleId { get; set; }
                public string EscrowTransactionRoleName => Enumeration.FromValue<EscrowTransactionRole>(EscrowTransactionRoleId).Name;
                public Guid EscrowTransactionId { get; set; }

                public Guid AccountId { get; set; }
                public string AccountName { get; set; }
            }
        }
    }
}