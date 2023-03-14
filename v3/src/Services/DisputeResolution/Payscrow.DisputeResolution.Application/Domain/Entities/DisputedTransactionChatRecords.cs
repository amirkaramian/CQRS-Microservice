using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.DisputeResolution.Application.Domain.Entities
{
    //Create Disputed Transaction Chat Records
    public class DisputedTransactionChatRecord : BaseEntity
    {
        public Guid DisputedTransactionChatRecordId { get; set; }
        public Guid AccountId { get; set; }
        public string DisputeChats { get; set; } // All chats on particular disputed transaction including payscrow dispute team, lawyers goes here
    }
}
