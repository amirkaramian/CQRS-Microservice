using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.DisputeResolution.Application.Domain.Entities
{
    // Create entity for disputed transactions
    public class DisputedTransaction : BaseEntity
    {


        public Guid DisputedTransactionId { get; set; }
        public Guid AccountId { get; set; }
        public Guid? TenantId { get; set; }
        public int StatusId { get; set; } 
        public bool InDispute { get; set; }
        public bool? InEscrow { get; set; }
        public int DisputeArbitrationLevelId { get; set; }  // This is an enum made up of "PayScrowDResolution Team = 1, Lawyer = 2"
        public Guid? DisputeArbitratorId { get; set; }  // Identity of the arbitrator on this dispute ticket e.g PayScrowDResolution Team
        public string DisputeDescription { get; set; }
      
    }
}
