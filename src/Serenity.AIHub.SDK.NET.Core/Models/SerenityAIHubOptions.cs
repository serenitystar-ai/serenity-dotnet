namespace Serenity.AIHub.SDK.NET.Core.Models;

/// <summary>
/// Options for the Serenity AI Hub.
/// </summary>
public class SerenityAIHubOptions
{
    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timeout in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;
}