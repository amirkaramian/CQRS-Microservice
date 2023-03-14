using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.Services;
using Payscrow.WebUI.ViewComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.ViewComponents
{
    public class CurrencySelectionViewComponent : ViewComponent
    {
        private readonly CurrencyService _currencyService;
        private readonly AccountSettingService _accountSettingService;

        public CurrencySelectionViewComponent(CurrencyService currencyService, AccountSettingService accountSettingService)
        {
            _currencyService = currencyService;
            _accountSettingService = accountSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currenciesResponse = await _currencyService.GetCurrencyModels();

            currenciesResponse.EnsureSuccessStatusCode();

            var currenciesResponseString = await currenciesResponse.Content.ReadAsStringAsync();

            var currencies = JsonConvert.DeserializeObject<List<CurrencySelectionViewModel.CurrencyModel>>(currenciesResponseString);

            var accountSettingResponse = await _accountSettingService.GetAccountSettingModel();

            accountSettingResponse.EnsureSuccessStatusCode();

            var accountSettingResponseString = await accountSettingResponse.Content.ReadAsStringAsync();

            var accountSetting = JsonConvert.DeserializeObject<CurrencySelectionViewModel.AccountSettingModel>(accountSettingResponseString);

            var model = new CurrencySelectionViewModel
            {
                AccountSetting = accountSetting
            };

            model.Currencies.AddRange(currencies);

            return View(model);
        }
    }
}