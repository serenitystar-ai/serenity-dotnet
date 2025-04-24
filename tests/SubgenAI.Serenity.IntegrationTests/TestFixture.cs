using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubgenAI.Serenity.Extensions;

namespace SubgenAI.Serenity.IntegrationTests;

public class TestFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; }
    public IConfiguration Configuration { get; }

    public TestFixture()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();

        var apiKey = Configuration["SerenityAIHub:ApiKey"]
            ?? throw new InvalidOperationException("API key not found in configuration");

        services.AddSerenityAIHub(apiKey);

        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}