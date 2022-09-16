using BoDi;

using Microsoft.AspNetCore.Mvc.Testing;

namespace Test.Acceptance.Hooks;

[Binding]
public class WeatherForecastHooks
{
    private readonly IObjectContainer _objectContainer;

    public WeatherForecastHooks(IObjectContainer objectContainer) =>
        _objectContainer = objectContainer ?? throw new ArgumentNullException(nameof(objectContainer));

    [BeforeScenario]
    public async Task RegisterServices()
    {
        var factory = GetWebApplicationFactory();
        await ClearData(factory);
        _objectContainer.RegisterInstanceAs(factory);
    }

    private WebApplicationFactory<Program> GetWebApplicationFactory() =>
        new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                //builder.ConfigureAppConfiguration((context, config) {});
                //builder.ConfigureTestServices(services => {});
            })
    ;

    private async Task ClearData(
        WebApplicationFactory<Program> factory)
    {
        //var service = factory.Services.GetService(typeof(IMyService));
        await Task.CompletedTask;
    }
}
