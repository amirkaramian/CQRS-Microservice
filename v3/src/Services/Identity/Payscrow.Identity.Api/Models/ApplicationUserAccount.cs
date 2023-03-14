using System;

namespace Payscrow.Identity.Api.Models
{
    public class ApplicationUserAccount : Entity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public DateTime? ExpirationUtc { get; set; }
        public bool Disabled { get; set; }
    }
}
