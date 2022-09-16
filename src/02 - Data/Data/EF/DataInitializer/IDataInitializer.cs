namespace Data.EF.DataInitializer;

public interface IDataInitializer
{
    ValueTask InitializeDataAsync(CancellationToken cancellationToken = default);
}
