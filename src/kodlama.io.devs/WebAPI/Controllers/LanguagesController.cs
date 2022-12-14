using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Commands.DeleteLanguage;
using Application.Features.Languages.Commands.UpdateLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetByIdLanguage;
using Application.Features.Languages.Queries.GetListLanguage;
using Application.Features.Languages.Queries.GetListLanguageByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
using static Application.Features.Languages.Queries.GetListLanguageByDynamic.GetListLanguageByDynamicQuery;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CreateLanguageCommand createLanguageCommand)
        {
            CreatedLanguageDto result = await Mediator.Send(createLanguageCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLanguageCommand updateLanguageCommand)
        {
            UpdatedLanguageDto result = await Mediator.Send(updateLanguageCommand);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteLanguageCommand deleteLanguageCommand)
        {
            DeletedLanguageDto result = await Mediator.Send(deleteLanguageCommand);
            return Ok($"{result.Name} silindi!");
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdLanguageQuery getByIdIdLanguageQuery)
        {
            LanguageGetByIdDto brandGetByIdDto = await Mediator.Send(getByIdIdLanguageQuery);
            return Ok(brandGetByIdDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListLanguageQuery getListLanguageQuery = new() { PageRequest = pageRequest };
            LanguageListModel result = await Mediator.Send(getListLanguageQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListLanguageByDynamicQuery getListByDynamicLanguageQuery = new GetListLanguageByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };
            LanguageListModel result = await Mediator.Send(getListByDynamicLanguageQuery);
            return Ok(result);

        }
    }
}
