namespace SpecFlowTests.PoC.WebApi.Customers;

using SpecFlowTests.PoC.WebApi.Customers.Interfaces;
using SpecFlowTests.PoC.WebApi.Customers.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddCustomers(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
