using System.Reflection;
using Serilog;
using SpecFlowTests.PoC.WebApi.Customers;
using SpecFlowTests.PoC.WebApi.Filters;

const int EXIT_FAILURE = 1;
const int EXIT_SUCCESS = 0;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(builder.Configuration)
        ;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddCustomers();

try
{
    Log.Information("Starting host...");

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapControllers();

    await app.RunAsync();

    return EXIT_SUCCESS;
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");

    return EXIT_FAILURE;
}
finally
{
    Log.CloseAndFlush();
}
