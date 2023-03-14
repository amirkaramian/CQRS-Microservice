using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Domain.Models
{
    public class User : Entity
    {
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarFileName { get; set; }
    }
}
