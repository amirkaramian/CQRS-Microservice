using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Application.Commands.ProjectMilestone
{
    public static class SendProjectCreateNotification
    {
        public class Command : IRequest<CommandResult>
        {

        }

        public class CommandResult : BaseCommandResult
        {

        }
    }
}
