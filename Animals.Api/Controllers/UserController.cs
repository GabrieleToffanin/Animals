using Animals.Core.Interfaces;
using Animals.Core.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Animals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusinessLogic _userService;
        public UserController(IUserBusinessLogic userService)
        {
            _userService = userService;
        }

        [HttpPost("/Register")]
        public async ValueTask<ActionResult<string>> Register(RegisterModel model)
        {
            var result = await _userService.RegisterUserAsync(model);
            return Ok(result);
        }

        [HttpPost("/Token")]
        public async ValueTask<IActionResult> GetToken(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("/AddRole")]
        public async ValueTask<IActionResult> AddRole(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }
    }
}
