using Microsoft.AspNetCore.Mvc;
using Payscrow.WebUI.Services;
using Payscrow.WebUI.ViewComponents.Models;
using System.Threading.Tasks;

namespace Payscrow.WebUI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UserService _userService;

        public HeaderViewComponent(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userModel = await _userService.GetUserModelAsync();

            var viewModel = new HeaderViewModel {
                User = userModel
            };

            return View(viewModel);
        }
    }
}
