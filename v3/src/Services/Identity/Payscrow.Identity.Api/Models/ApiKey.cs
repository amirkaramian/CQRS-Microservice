using Payscrow.Identity.Api.Enumerations;
using System;

namespace Payscrow.Identity.Api.Models
{
    public class ApiKey : Entity
    {
        public string Label { get; set; }
        public string Key { get; set; }
        public ApiKeyStatus Status { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}