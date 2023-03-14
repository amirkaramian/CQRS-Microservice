using System;

namespace Payscrow.MarketPlace.Application.Domain
{
    public abstract class AccountEntity : Entity
    {
        public Guid AccountId { get; set; }
    }
}
