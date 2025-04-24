namespace SubgenAI.Serenity.Models;

/// <summary>
/// Represents the response for creating a conversation.
/// </summary>
public class CreateConversationRes
{
    /// <summary>
    /// Unique identifier for the chat.
    /// </summary>
    public Guid ChatId { get; set; }

    /// <summary>
    /// Content of the conversation.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// List of conversation starters.
    /// </summary>
    public List<string> ConversationStarters { get; set; }

    /// <summary>
    /// Version of the agent
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Value indicating whether vision is used.
    /// </summary>
    public bool UseVision { get; set; }
}