using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Application.Domain.Models
{
    public class Currency : BaseModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }

        public decimal PercentageCharge { get; set; }
    }
}
