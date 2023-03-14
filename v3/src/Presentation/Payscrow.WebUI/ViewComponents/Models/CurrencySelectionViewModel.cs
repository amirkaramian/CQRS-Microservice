using System;
using System.Collections.Generic;

namespace Payscrow.WebUI.ViewComponents.Models
{
    public class CurrencySelectionViewModel
    {
        public List<CurrencyModel> Currencies { get; set; } = new List<CurrencyModel>();
        public AccountSettingModel AccountSetting { get; set; }

        public class AccountSettingModel
        {
            public string CurrencyCode { get; set; }
            public AccountSettingModel AccountSetting { get; set; }
        }

        public class CurrencyModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string Code { get; set; }
            public bool IsDefault { get; set; }
        }
    }
}