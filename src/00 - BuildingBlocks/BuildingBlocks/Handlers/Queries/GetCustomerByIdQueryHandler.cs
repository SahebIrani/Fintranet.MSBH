using AutoMapper;

using Domain.DTO;

using MediatR;

using Service.Interfaces;

namespace BuildingBlocks.Handlers.Queries;

public class GetCustomerByIdQuery : IRequest<CustomerDTO>
{
    public Guid CustomerId { get; }

    public GetCustomerByIdQuery(Guid customerId) => CustomerId = customerId;
}

public class GetNinjaByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
{
    public GetNinjaByIdQueryHandler(ICustomerService customerService, IMapper mapper)
    {
        CustomerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public ICustomerService CustomerService { get; }
    public IMapper Mapper { get; }

    public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var entitiy = await CustomerService.GetByIdAsync(cancellationToken, request.CustomerId);

        var customer = Mapper.Map<CustomerDTO>(entitiy);

        return customer;
    }
}
