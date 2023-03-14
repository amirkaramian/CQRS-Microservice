using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Web.HttpAggregator.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();
        string GetUserEmail();
        string GetUserPhone();
    }
}
