using Microsoft.Extensions.DependencyInjection;
using Serenity.AIHub.SDK.NET.Core.Client;
using Serenity.AIHub.SDK.NET.Core.Constants;
using Serenity.AIHub.SDK.NET.Core.Models;

namespace Serenity.AIHub.SDK.NET.Extensions;

/// <summary>
/// Provides extension methods for the IServiceCollection interface.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Serenity AI Hub client to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="apiKey">The API key to use for authentication.</param>
    /// <param name="timeoutSeconds">The timeout in seconds.</param>
    /// <returns>The service collection.</returns>
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