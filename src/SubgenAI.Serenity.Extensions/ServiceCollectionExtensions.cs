using Microsoft.Extensions.DependencyInjection;
using SubgenAI.Serenity.Client;
using SubgenAI.Serenity.Constants;
using SubgenAI.Serenity.Models;

namespace SubgenAI.Serenity.Extensions;

/// <summary>
/// Provides extension methods for the IServiceCollection interface.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Serenity AI Hub services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <param name="apiKey">The API key to use for authentication.</param>
    /// <param name="timeoutSeconds">Optional timeout in seconds. Defaults to 30 seconds.</param>
    /// <returns>The IServiceCollection with the Serenity AI Hub services added.</returns>
    public static IServiceCollection AddSerenityAIHub(
        this IServiceCollection services,
        string apiKey,
        int timeoutSeconds = 30)
    {
        services.Configure<SerenityAIHubOptions>(options =>
        {
            options.ApiKey = apiKey;
            options.TimeoutSeconds = timeoutSeconds;
        });

        services.AddHttpClient<ISerenityAIHubClient, SerenityAIHubClient>((_, client) =>
        {
            client.BaseAddress = new Uri(ClientConstants.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
        });

        return services;
    }
}