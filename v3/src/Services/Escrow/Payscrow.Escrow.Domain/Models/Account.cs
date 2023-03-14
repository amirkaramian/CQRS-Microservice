using Payscrow.Escrow.Domain.ValueObjects;
using System;

namespace Payscrow.Escrow.Domain.Models
{
    public class Account : Entity
    {
        public Guid AccountGuid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LogoFileName { get; set; }
        public Address Address { get; set; }
    }
}