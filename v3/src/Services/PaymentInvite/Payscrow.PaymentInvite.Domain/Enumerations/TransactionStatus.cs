using Payscrow.PaymentInvite.Domain.Exceptions;
using Payscrow.PaymentInvite.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payscrow.PaymentInvite.Domain.Enumerations
{
    public class TransactionStatus : Enumeration
    {
        //public static TransactionStatus AnonymousInvite = new TransactionStatus(1, nameof(AnonymousInvite).ToLowerInvariant(), "Anonymous Invite");
        //public static TransactionStatus VerifiedInvite = new TransactionStatus(2, nameof(VerifiedInvite).ToLowerInvariant(), "Verified Invite");

        public static TransactionStatus Pending = new TransactionStatus(1, nameof(Pending).ToLowerInvariant(), "Pending");
        public static TransactionStatus InProgress = new TransactionStatus(2, nameof(InProgress).ToLowerInvariant(), "In Progress");
        public static TransactionStatus ValueDelivered = new TransactionStatus(3, nameof(ValueDelivered).ToLowerInvariant(), "Value Delivered");
        public static TransactionStatus ValueReceived = new TransactionStatus(4, nameof(ValueReceived).ToLowerInvariant(), "Value Received");
        public static TransactionStatus InDespute = new TransactionStatus(5, nameof(InDespute).ToLowerInvariant(), "In Despute");
        public static TransactionStatus Completed = new TransactionStatus(6, nameof(Completed).ToLowerInvariant(), "Completed");


        protected TransactionStatus(int id, string name, string displayName) : base(id, name)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }


        public static IEnumerable<TransactionStatus> List() =>
            new[] { Pending, InProgress, ValueDelivered, ValueReceived, InDespute };


        public static TransactionStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new PaymentInviteDomainException($"Possible values for TransactionStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static TransactionStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new PaymentInviteDomainException($"Possible values for TransactionStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
