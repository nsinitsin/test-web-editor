using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Archangel.Tests.WebEditor.Controllers.News.ViewModels;
using Archangel.Tests.WebEditor.Infrastructure.Commands.News.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Archangel.Tests.WebEditor.Controllers.News
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IGetArchivedNewsCommand _getArchivedNewsCommand;
        private readonly IGetLiveNewsCommand _getLiveNewsCommand;
        private readonly IGetDraftNewsCommand _getDraftNewsCommand;
        private readonly ICreateDraftNewsCommand _createDraftNewsCommand;
        private readonly IActivateDraftNewsCommand _activateDraftNewsCommand;
        private readonly IUpdateActiveNewsCommand _updateActiveNewsCommand;
        private readonly IUpdateDraftNewsCommand _updateDraftNewsCommand;

        public NewsController(IGetArchivedNewsCommand getArchivedNewsCommand,
            IGetLiveNewsCommand getLiveNewsCommand,
            IGetDraftNewsCommand getDraftNewsCommand,
            ICreateDraftNewsCommand createDraftNewsCommand,
            IActivateDraftNewsCommand activateDraftNewsCommand,
            IUpdateActiveNewsCommand updateActiveNewsCommand,
            IUpdateDraftNewsCommand updateDraftNewsCommand)
        {
            _getArchivedNewsCommand = getArchivedNewsCommand;
            _getLiveNewsCommand = getLiveNewsCommand;
            _getDraftNewsCommand = getDraftNewsCommand;
            _createDraftNewsCommand = createDraftNewsCommand;
            _activateDraftNewsCommand = activateDraftNewsCommand;
            _updateActiveNewsCommand = updateActiveNewsCommand;
            _updateDraftNewsCommand = updateDraftNewsCommand;
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Get archived news")]
        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<ArchivedNewsViewModel>>> GetArchivedNews([FromQuery]int page, [FromQuery]int take)
        {
            var result = await _getArchivedNewsCommand.ExecuteAsync(page, take);

            if (!result.IsSuccessful)
                return StatusCode((int)result.HttpStatusCode, result.ReasonPhrase);

            return result.Result.Select(s=>new ArchivedNewsViewModel(s)).ToList();
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Get news which on live")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "System doesn't have any news in live")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "If more then 1 news in live on the server")]
        [HttpGet("live")]
        public async Task<ActionResult<NewsViewModel>> GetLastActiveNews()
        {
            var result = await _getLiveNewsCommand.ExecuteAsync();

            if (!result.IsSuccessful)
                return StatusCode((int)result.HttpStatusCode, result.ReasonPhrase);

            return new NewsViewModel(result.Result);
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Get news which on live")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Input data is invalid")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "News is not founded in DB for updating")]
        [HttpPut("live/{newsId}")]
        public async Task<ActionResult<long>> UpdateLiveNews([FromRoute] long newsId, [FromBody]NewsInputModel inputModel)
        {
            var result = await _updateActiveNewsCommand.ExecuteAsync(newsId, inputModel?.Text);

            if (!result.IsSuccessful)
                return StatusCode((int)result.HttpStatusCode, result.ReasonPhrase);

            return result.Result;
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Get draft news")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "System doesn't have any draft news")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "If more then 1 draft news on the server")]
        [HttpGet("draft")]
        public async Task<ActionResult<NewsViewModel>> GetDraftNews()
        {
            var result = await _getDraftNewsCommand.ExecuteAsync();

            if (!result.IsSuccessful)
                return StatusCode((int)result.HttpStatusCode, result.ReasonPhrase);

            return new NewsViewModel(result.Result);
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Get news which on live")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Input data is invalid")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "News is not founded in DB for updating")]
        [HttpPut("draft/{draftId}")]
        public async Task<ActionResult<long>> UpdateDraftNews([FromRoute] long draftId, [FromBody]NewsInputModel inputModel)
        {
            var result = await _updateDraftNewsCommand.ExecuteAsync(draftId, inputModel?.Text);

            if (!result.IsSuccessful)
                return StatusCode((int)result.HttpStatusCode, result.ReasonPhrase);

            return result.Result;
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Draft news has added")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "More then 1 draft news in the system")]
        [SwaggerResponse(StatusCodes.Status302Found, "Draft news is founded in DB. Only one draft news could be.")]
        [HttpPost]
        public async Task<ActionResult<long>> CreateDraftNews([FromBody]NewsInputModel inputModel)
        {
            var result = await _createDraftNewsCommand.ExecuteAsync(inputModel?.Text);

            if (!result.IsSuccessful)
                return BadRequest(result.ReasonPhrase);

            return result.Result;
        }

        [SwaggerResponse(StatusCodes.Status200OK, "News successfully added in live")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "System has already one active news in this week")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Draft news is not founded or invalid data")]
        [HttpPut("{newsId}")]
        public async Task<ActionResult> MoveDraftNewsToActive([FromRoute]long newsId)
        {
            var result = await _activateDraftNewsCommand.ExecuteAsync(newsId);

            if (!result.IsSuccessful)
                return StatusCode((int)result.HttpStatusCode, result.ReasonPhrase);

            return Ok();
        } 
    }
}