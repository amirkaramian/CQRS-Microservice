using System;

namespace Payscrow.ProjectMilestone.Application.Domain.Models
{
    public class PaymentLog : BaseModel
    {
        public decimal Amount { get; set; }

        public string PaymentNumber { get; set; }


        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public PaymentLog()
        {
            PaymentNumber = Common.Helpers.RandomHelper.GenerateUniqueNumbers();
        }
    }
}
