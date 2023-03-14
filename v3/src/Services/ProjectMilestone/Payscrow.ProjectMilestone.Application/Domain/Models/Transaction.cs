using Payscrow.ProjectMilestone.Application.Domain.Enumerations;
using System;

namespace Payscrow.ProjectMilestone.Application.Domain.Models
{
    public class Transaction : BaseModel
    {

        public Guid PayerAccountId { get; set; }
        public Guid ReceiverAccountId { get; set; }
    


        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public string Number { get; set; }


        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Transaction()
        {
            Number = Common.Helpers.RandomHelper.GenerateUniqueNumbers();
        }
    }
}
