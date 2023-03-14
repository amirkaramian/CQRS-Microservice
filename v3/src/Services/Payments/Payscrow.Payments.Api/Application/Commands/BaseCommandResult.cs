using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api.Application.Commands
{
    public class BaseCommandResult
    {
        public bool Success => Errors.Count == 0;
        public List<(string, string)> Errors { get; set; } = new List<(string, string)>();
    }
}