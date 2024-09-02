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
public class EmployeesController : ControllerBase
{
    // TODO: Custom ControllerBase
    // TODO: Custom ControllerBase for logging
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<List<Employee>> List()
    {
        var employees = _employeeService.List();

        if (!employees.Any())
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        else
        {
            return Ok(employees);
        }
    }
}