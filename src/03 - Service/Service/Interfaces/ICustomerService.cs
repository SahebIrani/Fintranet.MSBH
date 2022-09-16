using Data.EF.Repositories.Interfaces;

using Domain.Entities;

namespace Service.Interfaces;

public interface ICustomerService : IEFRepository<Customer>
{
    ValueTask<Customer[]> GetCustomersAsync(CancellationToken cancellationToken = default);

    ValueTask<string?> GetPhoneNumberAsync(string phoneNumberRaw, string countryCodeSelected);
}
