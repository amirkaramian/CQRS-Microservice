using System;

namespace Payscrow.MarketPlace.Application.Domain.Entities
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
