namespace SpecFlowTests.PoC.SmokeTests.Hooks;

using Microsoft.Extensions.Configuration;
using SpecFlowTests.PoC.SmokeTests.Drivers.Customers;
using TechTalk.SpecFlow;

[Binding]
internal sealed class ConfigurationHook
{
    [BeforeScenario]
    public static void SetupConfiguration(ScenarioContext scenarioContext)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        scenarioContext.Set(configuration.GetRequiredSection(nameof(CustomerApiDriverOptions)).Get<CustomerApiDriverOptions>());
    }
}
