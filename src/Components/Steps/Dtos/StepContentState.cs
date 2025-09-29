namespace Soenneker.Quark;

/// <summary>
/// Holds the information about the current state of the StepContent component.
/// </summary>
public sealed record StepContentState
{
    /// <summary>
    /// Gets the name of the selected panel.
    /// </summary>
    public string SelectedPanel { get; init; } = string.Empty;
}