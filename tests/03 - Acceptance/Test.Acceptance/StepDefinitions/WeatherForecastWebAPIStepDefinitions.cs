using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Presentation.Shared;

namespace Test.Acceptance.StepDefinitions;

[Binding]
public class WeatherForecastWebAPIStepDefinitions
{
    private const string BaseAddress = "https://localhost:5001/";
    public WebApplicationFactory<Program> Factory { get; }
    public HttpClient Client { get; set; } = null!;
    private HttpResponseMessage Response { get; set; } = null!;

    public WeatherForecastWebAPIStepDefinitions(WebApplicationFactory<Program> factory)
        => Factory = factory ?? throw new ArgumentNullException(nameof(factory));

    [Given(@"I'm a client")]
    public void GivenImAClient()
    {
        Client = Factory.CreateDefaultClient(new Uri(BaseAddress));
    }

    [Given(@"the repository has weather data")]
    public async Task GivenTheRepositoryHasWeatherData()
    {
        var forecasts = await Client.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
        Response = await Client.GetAsync($"WeatherForecast");
    }

    [Then(@"the response status code is '([^']*)'")]
    public void ThenTheResponseStatusCodeShouldBe(int statusCode)
    {
        var expected = (HttpStatusCode)statusCode;
        Assert.Equal(expected, Response.StatusCode);
    }

    [When(@"I make a GET request to '([^']*)'")]
    public void WhenIMakeAGETRequestTo(string weatherforecast)
    {
        throw new PendingStepException();
    }

    [Then(@"the response status code is '([^']*)'")]
    public void ThenTheResponseStatusCodeIs(string p0)
    {
        throw new PendingStepException();
    }

    [Then(@"the response json should be '([^']*)'")]
    public void ThenTheResponseJsonShouldBe(string p0)
    {
        throw new PendingStepException();
    }
}
