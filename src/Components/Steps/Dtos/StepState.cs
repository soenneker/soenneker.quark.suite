namespace Soenneker.Quark;

/// <summary>
/// Holds the information about the current state of the Steps component.
/// </summary>
public sealed record StepState
{
    /// <summary>
    /// Gets the name of the selected step item.
    /// </summary>
    public string SelectedStep { get; init; } = string.Empty;

    /// <summary>
    /// Gets the steps rendering mode.
    /// </summary>
    public StepRenderMode RenderMode { get; init; } = StepRenderMode.Default;
}
