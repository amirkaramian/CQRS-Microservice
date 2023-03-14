using System;

namespace Payscrow.Identity.Api.Models
{
    public class Account : Entity
    {
        public Account(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Account number. new accounts are assigned MAX(Number) + a random number between 1 and 9.
        /// </summary>
        public long Number { get; set; }

        
        public string Name { get; set; }

        public Guid OwnerUserId { get; set; }
        public ApplicationUser OwnerUser { get; set; }


        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public bool BusinessAccount { get; set; }


        /// <summary>
        /// If suspended, no acquisitions or dispositions can be committed to this account.
        /// </summary>
        public bool Suspended { get; set; }

        public bool Deleted { get; set; }

        /// <summary>
        /// When the user deletes an account, we make it visible for a period of time to allow them to still interact with
        /// it. After that period of time, we don't hard delete the account, but it is no longer visibile to the user.
        /// </summary>
        public DateTime? DeletePurgeUtc { get; set; }


        /// <summary>
        /// Key used to access the API.
        /// </summary>
        public string ApiKey { get; set; }
    }
}
