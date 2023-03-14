using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Payments.Api
{
    public class AppSettings
    {
        public PathSettings Paths { get; set; }
        public FlutterwaveSettings Flutterwave { get; set; }




        public class PathSettings
        {
            public string Logos { get; set; }
        }

        public class FlutterwaveSettings
        {
            public string BaseUrl { get; set; }
            public string SecretKey { get; set; }
        }
    }
}
