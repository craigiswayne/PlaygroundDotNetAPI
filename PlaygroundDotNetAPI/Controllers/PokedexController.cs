using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PlaygroundDotNetAPI.Models;
using PlaygroundDotNetAPI.Services;

namespace PlaygroundDotNetAPI.Controllers;

[ApiController]
[EnableRateLimiting("fixed-window")]
// [LogActionFilter] // TODO
[Route("[controller]")]
public class PokedexController(IPokedexService pokedexService) : ControllerBase
{
    // TODO: Custom ControllerBase
    // TODO: Custom ControllerBase for logging
    private readonly IPokedexService _pokedexService = pokedexService;

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