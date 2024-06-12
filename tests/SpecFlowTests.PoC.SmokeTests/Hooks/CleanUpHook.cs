namespace SpecFlowTests.PoC.SmokeTests.Hooks;

using SpecFlowTests.PoC.SmokeTests.Drivers.Customers;
using TechTalk.SpecFlow;

[Binding]
internal sealed class CleanUpHook
{
    [AfterScenario]
    public static async Task AfterCleanUp(ScenarioContext scenarioContext)
    {
        var customerApiDriver = new CustomerApiDriver(scenarioContext.Get<CustomerApiDriverOptions>());

        await customerApiDriver.CleanUpAsync();
    }
}
