using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payscrow.Identity.Api.Models
{
    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }


        public Guid TenantId { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime CreateUtc { get; set; }

        public Guid UpdateUserId { get; set; }
        public DateTime UpdateUtc { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }


        public Entity()
        {
            Id = SequentialGuid.GenerateComb();

            var utcNow = DateTime.UtcNow;
            CreateUtc = utcNow;
            UpdateUtc = utcNow;
        }
    }
}
