using System.Globalization;

namespace Presentation.Shared;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public override string ToString()
    {
        var result =
               $"Weather Forecast: [{nameof(Date)}: {Date.ToString(CultureInfo.CurrentCulture)}\n" +
               $"{nameof(TemperatureC)}: {TemperatureC}\n" +
               $"{nameof(TemperatureF)}: {TemperatureF}\n" +
               $"{nameof(Summary)}: {Summary} ]"
        ;

        return result;
    }
}
