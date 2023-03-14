using Microsoft.AspNetCore.Identity;
using System;

namespace Payscrow.Identity.Api.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser(Guid tenantId, string userName, string firstName, string lastName)
            : base($"{userName}_{tenantId}")
        {
            TenantId = tenantId;
            FirstName = firstName;
            LastName = lastName;

            Id = SequentialGuid.GenerateComb();
            var utcNow = DateTime.UtcNow;
            CreateUtc = utcNow;
            UpdateUtc = utcNow;
        }

        public Guid TenantId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public Guid CreateUserId { get; set; }
        public DateTime CreateUtc { get; set; }
        public Guid UpdateUserId { get; set; }
        public DateTime UpdateUtc { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}