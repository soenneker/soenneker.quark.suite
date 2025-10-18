namespace Soenneker.Quark;

/// <summary>
/// Holds the information about the current state of the <see cref="StepsContent"/> component.
/// </summary>
public sealed record StepsContentState
{
    /// <summary>
    /// Gets the name of the selected panel.
    /// </summary>
    public string SelectedPanel { get; init; } = string.Empty;
}


