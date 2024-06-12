namespace SpecFlowTests.PoC.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using SpecFlowTests.PoC.WebApi.Customers.Commands;
using SpecFlowTests.PoC.WebApi.Customers.Models;
using SpecFlowTests.PoC.WebApi.Customers.Queries;

[ApiController, Route("[controller]/[action]")]
public sealed class CustomersController : ApiControllerBase
{
    [HttpPost(Name = "AddCustomer")]
    public async Task<IActionResult> AddCustomer([FromBody] AddCustomer request, CancellationToken cancellationToken = default)
    {
        var id = await this.Mediator.Send(request, cancellationToken);

        return this.CreatedAtAction(nameof(this.GetCustomer), id);
    }

    [HttpDelete(Name = "DeleteCustomer")]
    public async Task<IActionResult> DeleteCustomer([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var request = new DeleteCustomer
        {
            Id = id,
        };

        await this.Mediator.Send(request, cancellationToken);

        return this.NoContent();
    }

    [HttpGet(Name = "GetCustomer")]
    public async Task<Customer> GetCustomer([FromQuery] Guid id, CancellationToken cancellationToken = default)
    {
        var request = new GetCustomer
        {
            Id = id,
        };

        return await this.Mediator.Send(request, cancellationToken);
    }

    [HttpGet(Name = "GetCustomers")]
    public async Task<IReadOnlyList<Customer>> GetCustomers(CancellationToken cancellationToken = default)
    {
        var request = new GetCustomers();

        return await this.Mediator.Send(request, cancellationToken);
    }
}
