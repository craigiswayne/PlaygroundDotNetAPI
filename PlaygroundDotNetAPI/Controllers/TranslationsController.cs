using System.Globalization;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PlaygroundDotNetAPI.Helpers;
using PlaygroundDotNetAPI.Models;
using CsvHelper;
using CsvHelper.Configuration;


namespace PlaygroundDotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TranslationsController : ControllerBase
{
    // TODO
    // split up main csv into translation per lang
    [HttpGet("{languageIso}")]
    [ResponseCache(Duration = TimeHelper.OneMinuteInSeconds, Location = ResponseCacheLocation.Any)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public ActionResult<List<TranslationItemResponse>> List(string languageIso = "es")
    {
        var fileName = $"translations_{languageIso}.csv";
        var filePath = $"{Environment.CurrentDirectory}\\Data\\translations\\{fileName}";

        if (!System.IO.File.Exists(filePath))
        {
            return NoContent();
        }

        var csvReaderConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };
        try
        {
            List<dynamic> csvRecords;
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, csvReaderConfig))
            {
                csvRecords = csv.GetRecords<dynamic>().ToList();
            }

            return Ok(csvRecords);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}