using System.Net;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Test.Acceptance.StepDefinitions;

[Binding]
public class WeatherForecastWebAPIStepDefinitions
{
    private const string BaseAddress = "http://localhost/";
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
    public void GivenTheRepositoryHasWeatherData()
    {
        //var forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
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
