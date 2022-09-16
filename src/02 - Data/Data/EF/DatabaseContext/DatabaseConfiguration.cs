using Microsoft.Extensions.Configuration;

namespace Data.EF.DatabaseContext;

public class DatabaseConfiguration : ConfigurationBase
{
    private const string DefaultConnection = nameof(DefaultConnection);
    public string GetDataConnectionString() => GetConfiguration().GetConnectionString(DefaultConnection);
}
