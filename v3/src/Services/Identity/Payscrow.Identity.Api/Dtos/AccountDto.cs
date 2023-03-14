using System;

namespace Payscrow.Identity.Api.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public string Name { get; set; }
        public bool IsVerified { get; set; }
    }
}
