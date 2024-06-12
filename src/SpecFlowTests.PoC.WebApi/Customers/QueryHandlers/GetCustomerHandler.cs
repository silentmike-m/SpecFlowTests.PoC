namespace SpecFlowTests.PoC.WebApi.Customers.QueryHandlers;

using SpecFlowTests.PoC.WebApi.Customers.Exceptions;
using SpecFlowTests.PoC.WebApi.Customers.Interfaces;
using SpecFlowTests.PoC.WebApi.Customers.Models;
using SpecFlowTests.PoC.WebApi.Customers.Queries;

internal sealed class GetCustomerHandler : IRequestHandler<GetCustomer, Customer>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<GetCustomerHandler> logger;

    public GetCustomerHandler(ICustomerRepository customerRepository, ILogger<GetCustomerHandler> logger)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task<Customer> Handle(GetCustomer request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get customer with id {CustomerId}", request.Id);

        var customer = await this.customerRepository.GetCustomerAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            throw new CustomerNotFoundException(request.Id);
        }

        return customer;
    }
}
