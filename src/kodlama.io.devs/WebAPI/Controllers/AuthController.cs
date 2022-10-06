using Application.Features.Authorization.Commands.AuthLogin;
using Application.Features.Authorization.Commands.AuthRegister;
using Core.Security.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            AuthorizationRegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAdress = GetIpAddress()
            };

            var result = await Mediator.Send(registerCommand);
            return Created("", result.AccessToken);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthorizationLoginCommand loginCommand)
        {
            var result = await Mediator.Send(loginCommand);
            return Ok(result);
        }
    }
}
