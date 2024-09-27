using PlaygroundDotNetAPI.Controllers;
using PlaygroundDotNetAPI;
using Microsoft.Extensions.Logging;
using Moq;

namespace PlaygroundDotNetAPI.Tests;

[TestFixture]
public class ProductsControllerTests
{
    // private Mock<ILogger<WeatherForecastController>> _mockLogger;
    private WeatherForecastController _controller;

    [SetUp]
    public void Setup()
    {
        // _mockLogger = new Mock<ILogger<WeatherForecastController>>();
        // _controller = new WeatherForecastController(_mockLogger.Object);
        _controller = new WeatherForecastController();
    }

    [Test]
    public void GetWeatherForecast()
    {
        var result = _controller.Get();
        Assert.That(result, Is.InstanceOf<WeatherForecast[]>(), "Result should be an array of WeatherForecast");
        var expectedCount = 5;
        Assert.That(result, Has.Exactly(expectedCount).Items, $"Array should contain {expectedCount} Item");
    }
}