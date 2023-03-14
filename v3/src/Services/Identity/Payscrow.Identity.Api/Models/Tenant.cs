using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Models
{
    public class Tenant 
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string DomainName { get; set; }

        public string Colour { get; set; }
        public string LogoUrl { get; set; }


        public Guid CreateUserId { get; set; }
        public DateTime CreateUtc { get; set; }

        public Guid? UpdateUserId { get; set; }
        public DateTime? UpdateUtc { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }


        public Tenant()
        {
            Id = SequentialGuid.GenerateComb();

            var utcNow = DateTime.UtcNow;
            CreateUtc = utcNow;
            UpdateUtc = utcNow;
        }
    }
}
