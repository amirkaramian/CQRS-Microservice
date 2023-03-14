using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI
{
    public class AppSettings
    {
        public UrlsSettings Urls { get; set; }


        public class UrlsSettings {
            public string PaymentInvite { get; set; }
            public string Payments { get; set; }
            public string Escrow { get; set; }
            public string MarketPlace { get; set; }
        }
    }
}
