using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using playground_dotnet_api.Models;
using playground_dotnet_api.Services;

namespace playground_dotnet_api.Controllers;

[ApiController]
// [LogActionFilter] // TODO
[Route("[controller]")]
public class PokedexController : ControllerBase
{
    // TODO: Custom ControllerBase
    // TODO: Custom ControllerBase for logging
    private readonly IPokedexService _pokedexService;

    public PokedexController(IPokedexService pokedexService)
    {
        _pokedexService = pokedexService;
    }
    
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<List<Pokemon>> List()
    {
        var pokemon = _pokedexService.List();

        if (!pokemon.Any())
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        else
        {
            return Ok(pokemon);
        }
    }
}