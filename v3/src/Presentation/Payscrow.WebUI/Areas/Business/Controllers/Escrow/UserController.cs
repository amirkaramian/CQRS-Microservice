using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payscrow.WebUI.BindingModels;
using Payscrow.WebUI.Services;
using System.Net;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Controllers.Escrow
{
    public class UserController : BaseEscrowApiController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _userService.GetUserModelAsync());
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] UserBindingModel model)
        {
            var response = await _userService.UpdateUserAsync(model);

            var updateResponse = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(updateResponse));
        }
    }
}