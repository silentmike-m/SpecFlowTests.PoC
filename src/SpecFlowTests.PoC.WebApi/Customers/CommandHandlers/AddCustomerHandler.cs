namespace SpecFlowTests.PoC.WebApi.Customers.CommandHandlers;

using SpecFlowTests.PoC.WebApi.Customers.Commands;
using SpecFlowTests.PoC.WebApi.Customers.Interfaces;
using SpecFlowTests.PoC.WebApi.Customers.Models;

internal sealed class AddCustomerHandler : IRequestHandler<AddCustomer, Guid>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<AddCustomerHandler> logger;

    public AddCustomerHandler(ICustomerRepository customerRepository, ILogger<AddCustomerHandler> logger)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task<Guid> Handle(AddCustomer request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to add customer");

        var customer = new Customer
        {
            FirstName = request.FirstName,
            Id = Guid.NewGuid(),
            LastName = request.LastName,
        };

        await this.customerRepository.AddCustomerAsync(customer, cancellationToken);

        return customer.Id;
    }
}
