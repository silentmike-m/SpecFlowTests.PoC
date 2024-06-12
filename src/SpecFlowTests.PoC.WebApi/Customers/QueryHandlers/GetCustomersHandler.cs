namespace SpecFlowTests.PoC.WebApi.Customers.QueryHandlers;

using SpecFlowTests.PoC.WebApi.Customers.Interfaces;
using SpecFlowTests.PoC.WebApi.Customers.Models;
using SpecFlowTests.PoC.WebApi.Customers.Queries;

internal sealed class GetCustomersHandler : IRequestHandler<GetCustomers, IReadOnlyList<Customer>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<GetCustomersHandler> logger;

    public GetCustomersHandler(ICustomerRepository customerRepository, ILogger<GetCustomersHandler> logger)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task<IReadOnlyList<Customer>> Handle(GetCustomers request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get customers");

        return await this.customerRepository.GetCustomersAsync(cancellationToken);
    }
}
