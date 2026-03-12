
namespace Soenneker.Quark;

/// <summary>
/// Context for step navigation events.
/// </summary>
public sealed record StepNavigationContext
{
    /// <summary>
    /// Gets the current step name.
    /// </summary>
    public string CurrentStepName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the current step index.
    /// </summary>
    public int CurrentStepIndex { get; init; }

    /// <summary>
    /// Gets the next step name.
    /// </summary>
    public string NextStepName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the next step index.
    /// </summary>
    public int NextStepIndex { get; init; }
}
