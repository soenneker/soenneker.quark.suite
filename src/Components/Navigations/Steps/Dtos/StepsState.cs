namespace Soenneker.Quark;

/// <summary>
/// Holds the information about the current state of the <see cref="Steps"/> component.
/// </summary>
public sealed record StepsState
{
    /// <summary>
    /// Gets the name of the selected step item.
    /// </summary>
    public string SelectedStep { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the steps rendering mode.
    /// </summary>
    public StepsRenderMode RenderMode { get; init; } = StepsRenderMode.Default;
}


