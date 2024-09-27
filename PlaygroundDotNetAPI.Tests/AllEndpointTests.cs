using System.Net;
using System.Net.Http.Headers;

namespace PlaygroundDotNetAPI.Tests;

[TestFixture("https://localhost:7068/environment")]
[TestFixture("https://localhost:7068/pokedex")]
[Parallelizable(ParallelScope.All)]
public class AllEndpointTests
{
    private string _url;

    public AllEndpointTests(string url)
    {
        _url = url;
    }

    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        _client = new HttpClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }

    [Test]
    [TestCase("Content-Security-Policy", "default-src 'self';")]
    // [TestCase("https://localhost:7068/pokedex")]
    // public void TestTheHeaders(string endpointUrl)
    public void TestTheHeaders(string headerKey, string headerValue)
    {

        var expectedHeaders = new Dictionary<string, string>
        {
            { "Content-Security-Policy", "default-src 'self';" },
            { "Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()" },
            { "Referrer-Policy", "same-origin" },
            { "X-Content-Type-Options", "nosniff" },
            { "X-Frame-Options", "DENY" },
            { "X-Permitted-Cross-Domain-Policies", "none" },
            { "X-Xss-Protection", "1; mode=block" }
        };

        string[] excludedHeaders =
        [
            "X-Powered-By",
            "Server"
        ];
        
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(_url),
            Method = HttpMethod.Get
        };

        var response = _client.SendAsync(request).Result;
        var headers = response.Headers;

        foreach (var excludedHeader in excludedHeaders)
        {
            Assert.IsTrue(!headers.Contains(excludedHeader), $"Headers should NOT have property: {excludedHeader}");
        }
            
        foreach (KeyValuePair<string, string> header in expectedHeaders)
        {
            Assert.IsTrue(headers.Contains(header.Key), $"Headers should have property: {header.Key}");
        }
    }
    
}