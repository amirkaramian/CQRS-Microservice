using System;
using System.ComponentModel.DataAnnotations;

namespace Payscrow.Identity.Api.Dtos
{
    public class QuickCreateAccountDto
    {
        //[Required, EmailAddress]
        public string EmailAddress { get; set; }

        //[Required]
        public string Name { get; set; }

        //[Required]
        public string PhoneNumber { get; set; }

        public Guid TenantId { get; set; }
    }
}