
namespace Soenneker.Quark;

/// <summary>
/// Legacy state for older Quark Steps; kept for compatibility within this repo until removed.
/// </summary>
public sealed record StepState
{
    /// <summary>
    /// Gets the name of the selected step item.
    /// </summary>
    public string SelectedStep { get; init; } = string.Empty;

    /// <summary>
    /// Gets the step rendering mode.
    /// </summary>
    public StepRenderMode RenderMode { get; init; } = StepRenderMode.Default;
}
