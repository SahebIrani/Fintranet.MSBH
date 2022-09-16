using AutoMapper;

using Domain.DTO;

using MediatR;

using Service.Interfaces;

namespace BuildingBlocks.Handlers.Queries;

public class GetAllCustomerQuery : IRequest<IEnumerable<CustomerDTO>>
{
}

public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<CustomerDTO>>
{
    public GetAllCustomerQueryHandler(ICustomerService customerService, IMapper mapper)
    {
        CustomerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public ICustomerService CustomerService { get; }
    public IMapper Mapper { get; }

    public async Task<IEnumerable<CustomerDTO>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var entities = await CustomerService.GetAllAsync(cancellationToken);

        var customers = Mapper.Map<IEnumerable<CustomerDTO>>(entities);

        return customers;
    }
}
