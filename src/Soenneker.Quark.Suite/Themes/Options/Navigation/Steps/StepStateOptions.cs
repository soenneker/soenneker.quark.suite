namespace Soenneker.Quark;

/// <summary>
/// Configuration options for step state styling (active or non-active).
/// </summary>
public sealed class StepStateOptions
{
    /// <summary>
    /// Gets or sets the background color for the step marker.
    /// </summary>
    public string? MarkerBg { get; set; }

    /// <summary>
    /// Gets or sets the border color for the step marker.
    /// </summary>
    public string? MarkerBorder { get; set; }

    /// <summary>
    /// Gets or sets the text color for the step marker.
    /// </summary>
    public string? MarkerText { get; set; }

    /// <summary>
    /// Gets or sets the text color for the step link and captions.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the background color for the step link.
    /// </summary>
    public string? Bg { get; set; }
}