using Payscrow.MarketPlace.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payscrow.MarketPlace.Application.Domain.Enumerations
{
    public class TransactionStatus : Enumeration
    {
        public static TransactionStatus Pending = new(1, nameof(Pending).ToLowerInvariant(), "Pending");
        public static TransactionStatus InProgress = new(2, nameof(InProgress).ToLowerInvariant(), "In Progress");
        public static TransactionStatus Completed = new(3, nameof(Completed).ToLowerInvariant(), "Completed");
        public static TransactionStatus Finalized = new(4, nameof(Finalized).ToLowerInvariant(), "Finalized");

        protected TransactionStatus(int id, string name, string displayName) : base(id, name)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }

        public static IEnumerable<TransactionStatus> List() =>
            new[] { Pending, InProgress, Completed, Finalized };

        public static TransactionStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new MarketPlaceDomainException($"Possible values for Transaction Status: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static TransactionStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new MarketPlaceDomainException($"Possible values for Transaction Status: {string.Join(",", List().Select(s => s.Id))}");
            }

            return state;
        }
    }
}