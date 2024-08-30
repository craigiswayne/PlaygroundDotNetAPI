using PlaygroundDotNetAPI.Controllers;
using PlaygroundDotNetAPI;
using Microsoft.Extensions.Logging;
using Moq;

namespace PlaygroundDotNetAPI.Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<ILogger<WeatherForecastController>> _mockLogger;
        private WeatherForecastController _controller;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();
            _controller = new WeatherForecastController(_mockLogger.Object);
        }

        [Test]
        public void GetWeatherForecast()
        {
            IEnumerable<WeatherForecast> result = _controller.Get();
            Assert.That(result, Is.InstanceOf<WeatherForecast[]>(), "Result should be an array of WeatherForecast");
            int expected_count = 5;
            Assert.That(result, Has.Exactly(expected_count).Items, $"Array should contain {expected_count} Item");
        }
    }
}
