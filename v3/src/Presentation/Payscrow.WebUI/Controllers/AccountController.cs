using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Payscrow.WebUI.Services;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly AccountSettingService _accountSettingService;

        public AccountController(AccountSettingService accountSettingService)
        {
            _accountSettingService = accountSettingService;
        }

        public IActionResult Login(string returnUrl)
        {
            return Challenge(new AuthenticationProperties {
                //RedirectUri = string.IsNullOrWhiteSpace(returnUrl) ? "/business" : returnUrl
                RedirectUri = "/business"
            }, OpenIdConnectDefaults.AuthenticationScheme);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            // "Catalog" because UrlHelper doesn't support nameof() for controllers
            // https://github.com/aspnet/Mvc/issues/5853
            //var homeUrl = Url.Action(nameof(CatalogController.Index), "Catalog");
            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = "/" });
        }

        //public IActionResult Logout()
        //{

        //    return SignOut(new AuthenticationProperties
        //    {
        //        RedirectUri = "/"
        //    }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        //}


        public async Task<ActionResult> ChangeCurrency(string code, string returnUrl)
        {
            await _accountSettingService.ChangeCurrencyAsync(code);

            return LocalRedirect(returnUrl);
        }
    }   

}
