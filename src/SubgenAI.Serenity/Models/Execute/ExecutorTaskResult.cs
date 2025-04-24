namespace SubgenAI.Serenity.Models.Execute;

/// <summary>
/// Represents the result of an executor task.
/// </summary>
public class ExecutorTaskResult
{
    /// <summary>
    /// Gets or sets the description of the executor task result.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the duration of the executor task result in milliseconds.
    /// </summary>
    public long Duration { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the executor task result was successful.
    /// </summary>
    public bool Success { get; set; }
}