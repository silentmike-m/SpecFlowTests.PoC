namespace SpecFlowTests.PoC.WebApi.Customers.CommandHandlers;

using SpecFlowTests.PoC.WebApi.Customers.Commands;
using SpecFlowTests.PoC.WebApi.Customers.Interfaces;

internal sealed class DeleteCustomerHandler : IRequestHandler<DeleteCustomer>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<DeleteCustomerHandler> logger;

    public DeleteCustomerHandler(ICustomerRepository customerRepository, ILogger<DeleteCustomerHandler> logger)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task Handle(DeleteCustomer request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to delete customer with id {CustomerId}", request.Id);

        var customer = await this.customerRepository.GetCustomerAsync(request.Id, cancellationToken);

        if (customer is not null)
        {
            await this.customerRepository.DeleteCustomerAsync(customer.Id, cancellationToken);
        }
    }
}
