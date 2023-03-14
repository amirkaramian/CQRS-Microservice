using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Domain.Models
{
    public class Note : Entity
    {
        public string Content { get; set; }


        public NoteType Type { get; set; }

        public Guid? TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}


