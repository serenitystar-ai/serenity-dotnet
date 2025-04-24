using System.Text.Json;

namespace Serenity.AIHub.SDK.NET.Core.Models.Execute;

/// <summary>
/// Represents the result of an agent execution.
/// </summary>
public class AgentResult
{
    /// <summary>
    /// The response from the agent.
    /// Typically, an AI generate answer.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// If the content is a JSON object, the deserialized object representation of it.
    /// </summary>
    public JsonElement? JsonContent { get; set; }

    /// <summary>
    /// Representation of the token counts processed for a completions request.
    /// </summary>
    public AgentResultTokenUsage CompletionUsage { get; set; }

    /// <summary>
    /// The results of the actions performed by the Agent, if any.
    /// Such as the response from Plugins executed or other Agents invoked.
    /// </summary>
    public Dictionary<string, object> ActionResults { get; set; }

    /// <summary>
    /// The time taken to generate the first token.
    /// </summary>
    public long? TimeToFirstToken { get; set; }

    /// <summary>
    /// The instance id of the agent.
    /// </summary>
    public Guid InstanceId { get; set; }

    /// <summary>
    /// The tasks executed to complete the request and their duration in milliseconds.
    /// </summary>
    public List<ExecutorTaskResult> ExecutorTaskLogs { get; set; }
}