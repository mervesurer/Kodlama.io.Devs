using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfileByDynamic;
using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Commands.DeleteLanguage;
using Application.Features.Languages.Commands.UpdateLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetListLanguage;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubProfileController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGitHubProfileCommand createGitHubProfileCommand)
        {
            CreatedGitHubProfileDto result = await Mediator.Send(createGitHubProfileCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGitHubProfileCommand updateGitHubProfileCommand)
        {
            UpdatedGitHubProfileDto result = await Mediator.Send(updateGitHubProfileCommand);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGitHubProfileCommand deleteGitHubProfileCommand)
        {
            DeletedGitHubProfileDto result = await Mediator.Send(deleteGitHubProfileCommand);
            return Ok($"{result.GitHubUrl} silindi!");
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGitHubProfileQuery getListGitHubProfileQuery = new() { PageRequest = pageRequest };
            GitHubProfileListModel result = await Mediator.Send(getListGitHubProfileQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListGitHubProfileByDynamicQuery getListByDynamicModelQuery = new GetListGitHubProfileByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };
            GitHubProfileListModel result = await Mediator.Send(getListByDynamicModelQuery);
            return Ok(result);

        }
    }
}
