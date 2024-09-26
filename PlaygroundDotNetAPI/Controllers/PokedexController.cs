using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PlaygroundDotNetAPI.Models;
using PlaygroundDotNetAPI.Services;

namespace PlaygroundDotNetAPI.Controllers;

[ApiController]
[EnableRateLimiting("fixed-window")]
[Route("[controller]")]
public class PokedexController(IPokedexService pokedexService) : ControllerBase
{

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<List<Pokemon>> List()
    {
        var pokemon = pokedexService.List();

        if (!pokemon.Any())
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        return Ok(pokemon);
    }
}