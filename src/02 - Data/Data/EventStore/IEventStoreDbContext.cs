using EventStore.ClientAPI;

namespace Data.EventStore;

public interface IEventStoreDbContext
{
    Task<IEventStoreConnection> GetConnection();

    Task AppendToStreamAsync(params EventData[] events);
}