using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.DisputeResolution.Application.Domain.Entities
{
    public class DisputeArbitrator : BaseEntity
    {
        public Guid DisputeArbitratorId { get; set; }
        public Guid? ProjectOwnerRepAccountId { get; set; }
        public Guid? VendorRepAccountId { get; set; }
    }
}
