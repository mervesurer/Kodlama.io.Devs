using Application.Features.Authorization.Commands.AuthLogin;
using Application.Features.Authorization.Commands.AuthRegister;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AuthorizationRegisterCommand authRegisterCommand)
        {
            var result = await Mediator.Send(authRegisterCommand);
            return Created("", result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthorizationLoginCommand loginCommand)
        {
            var result = await Mediator.Send(loginCommand);
            return Ok(result);
        }
    }
}
