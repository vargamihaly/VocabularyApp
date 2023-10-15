using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using VocabularyApp.Api.Areas.Web.Words.Response;
using VocabularyApp.Api.Routing;
using VocabularyApp.Application.Services.Words;

namespace VocabularyApp.Api.Areas.Web.Words;

[ApiRoute]
[Produces("application/json")]
[ApiController]
public class WordController : ControllerBase
{
    private readonly IWordService _wordService;

    public WordController(IWordService wordService)
    {
        this._wordService = wordService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(HttpStatusCode.OK, typeof(GetWordListItemResponse))]
    [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
    [HttpGet]
    [Route("GetWordList")]
    public async Task<ActionResult<GetWordListItemResponse>> GetWordList()
    {
        var words = await _wordService.GetAllWordsAsync();

        var response = new GetWordListResponse();

        foreach (var word in words)
        {
            response.WordList.Add(new GetWordListItemResponse()
            {
                WordTitle = word.WordTitle,
                Description = word.Description,
            });
        }

        return Ok(response);
    }



    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [SwaggerResponse(HttpStatusCode.OK, typeof(void))]
    // [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
    // [HttpDelete]
    // [Route("DeleteWord")]
    // public async Task<ActionResult> DeleteWord(int id)
    // {
    //     _ = await _wordService.DeleteWordAsync(id);
    //
    //     return Ok();
    // }
}
