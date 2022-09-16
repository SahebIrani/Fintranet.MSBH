using BoDi;

using Microsoft.AspNetCore.Mvc.Testing;

namespace Test.Acceptance.Hooks;

[Binding]
public sealed class WeatherForecastHooks
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

    private static readonly DotNetCoreHost Host =
    new DotNetCoreHost(new DotNetCoreHostOptions
    {
        Port = HostConstants.Port,
        CsProjectPath = HostConstants.CsProjectPath
    });

    [BeforeFeature("api")]
    public static void StartHost()
    {
        Host.Start();
    }

    [AfterFeature("api")]
    public static void ShutdownHost()
    {
        Host.Stop();
    }
}
