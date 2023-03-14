using Microsoft.EntityFrameworkCore;
using Payscrow.PaymentInvite.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Services
{
    public class TransactionService : ISelfTransientLifetime
    {
        private readonly IPaymentInviteDbContext _context;

        public TransactionService(IPaymentInviteDbContext context)
        {
            _context = context;
        }

        const string TRANSACTION_NO_PREFIX = "DLTX_";

        public async Task<long> GetNextTransactionNumberAsync()
        {
            // Brand new site with no legacy data. Start from 10,000. Lowest possible account number is 10,001.
            const long INITIAL_ACCOUNT_NUMBER = 10_000L;

            // If this is the first account, start at INITIAL_ACCOUNT_NUMBER and add a random offset.
            var currentMaxNumber = await _context.Transactions
                .MaxAsync(ca => (long?)ca.Number) ?? INITIAL_ACCOUNT_NUMBER;

            return currentMaxNumber + GetRandomOffset();
        }

        private static readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(RandomHelper.CreateRandom);

        /// <summary>
        /// We don't want people to look at our numbers and be able to guess the number of transactions we have, or to be
        /// able to sequentially guess each transaction numbers, so generate a random offset to add to new 
        /// transaction numbers.
        /// </summary>
        private long GetRandomOffset()
        {
            // The upper bound is exclusive, so this will return a random number between 1 and 9, inclusive.
            return _random.Value!.Next(1, 10);
        }
    }
}
