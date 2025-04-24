using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using SubgenAI.Serenity.Constants;
using SubgenAI.Serenity.Models;
using SubgenAI.Serenity.Models.Execute;

namespace SubgenAI.Serenity.Client;

/// <inheritdoc />
public class SerenityAIHubClient : ISerenityAIHubClient
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="SerenityAIHubClient"/> class for dependency injection.
    /// </summary>
    /// <param name="options">The options containing the API key.</param>
    /// <param name="httpClient">The HTTP client to use for making requests.</param>
    public SerenityAIHubClient(IOptions<SerenityAIHubOptions> options, HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(options);
        string apiKey = options.Value.ApiKey ?? throw new ArgumentNullException(nameof(options), "The ApiKey property of the options. Value object is null.");
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        ConfigureHttpClient(_httpClient, apiKey);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SerenityAIHubClient"/> class for direct instantiation.
    /// </summary>
    /// <param name="apiKey">The API key to use for authentication.</param>
    /// <param name="baseUrl">The base URL to use for the API.</param>
    /// <param name="timeout">The timeout in seconds.</param>
    public static SerenityAIHubClient Create(string apiKey, string baseUrl = null, int? timeout = null)
    {
        if (string.IsNullOrEmpty(apiKey))
            throw new ArgumentNullException(nameof(apiKey));

        HttpClient httpClient = new();
        ConfigureHttpClient(httpClient, apiKey, baseUrl, timeout);

        return new SerenityAIHubClient(httpClient);
    }

    // Private constructor for direct instantiation
    private SerenityAIHubClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private static void ConfigureHttpClient(HttpClient client, string apiKey, string baseUrl = null, int? timeout = null)
    {
        client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        baseUrl ??= ClientConstants.BaseUrl;
        client.BaseAddress = new Uri(baseUrl);
        client.Timeout = TimeSpan.FromSeconds(timeout ?? 100);
    }

    /// <inheritdoc />
    public async Task<CreateConversationRes> CreateConversation(
    string agentCode,
    List<ExecuteParameter> inputParameters = null,
    int? agentVersion = null,
    int apiVersion = 2,
    CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(agentCode))
            throw new ArgumentNullException(nameof(agentCode));

        inputParameters ??= [];

        string version = agentVersion.HasValue ? $"/{agentVersion}" : string.Empty;

        // Create the content with input parameters
        var requestBody = new
        {
            inputParameters
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            $"/api/v{apiVersion}/agent/{agentCode}/conversation{version}",
            requestBody,
            JsonOptions,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {errorContent}");
        }

        return await response.Content.ReadFromJsonAsync<CreateConversationRes>(JsonOptions, cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    /// <inheritdoc />
    public async Task<AgentResult> Execute(string agentCode, List<ExecuteParameter> input = null, int? agentVersion = null, int apiVersion = 2, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(agentCode))
            throw new ArgumentNullException(nameof(agentCode));

        input ??= [];

        string version = agentVersion.HasValue ? $"/{agentVersion}" : string.Empty;

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            $"/api/v{apiVersion}/agent/{agentCode}/execute{version}",
            input,
            JsonOptions,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {errorContent}");
        }

        return await response.Content.ReadFromJsonAsync<AgentResult>(JsonOptions, cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize response");
    }
}