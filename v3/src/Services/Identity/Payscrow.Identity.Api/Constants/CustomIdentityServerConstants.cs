using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Constants
{
    public static class CustomIdentityServerConstants
    {
        public static string IDENTITY_SERVER_SECRET = "secret";

        public static class Scopes
        {
            public static string ROLE = "role";
            public static string API_READ = "api.read";
        }
    }
}