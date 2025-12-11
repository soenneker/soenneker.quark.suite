
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for steps component styling.
/// </summary>
public sealed class StepsOptions : ComponentOptions
{
    /// <summary>
    /// Initializes a new instance of the StepsOptions class.
    /// </summary>
    public StepsOptions()
    {
        Selector = ".q-steps";
    }

    /// <summary>
    /// Gets or sets the connector color between steps.
    /// </summary>
    public string? ConnectorColor { get; set; }
    /// <summary>
    /// Gets or sets the background color for active step markers.
    /// </summary>
    public string? MarkerActiveBg { get; set; }
    /// <summary>
    /// Gets or sets the border color for active step markers.
    /// </summary>
    public string? MarkerActiveBorder { get; set; }
    /// <summary>
    /// Gets or sets the text color for active step markers.
    /// </summary>
    public string? MarkerActiveColor { get; set; }
    /// <summary>
    /// Gets or sets the text color for active steps.
    /// </summary>
    public string? TextActive { get; set; }
    /// <summary>
    /// Gets or sets the success state color.
    /// </summary>
    public string? Success { get; set; }
    /// <summary>
    /// Gets or sets the background color for disabled steps.
    /// </summary>
    public string? DisabledBg { get; set; }
    /// <summary>
    /// Gets or sets the text color for disabled steps.
    /// </summary>
    public string? DisabledColor { get; set; }
    /// <summary>
    /// Gets or sets the background color for step content.
    /// </summary>
    public string? ContentBg { get; set; }
    /// <summary>
    /// Gets or sets the border color for step content.
    /// </summary>
    public string? ContentBorder { get; set; }
    /// <summary>
    /// Gets or sets the border radius for step content.
    /// </summary>
    public string? ContentRadius { get; set; }
    /// <summary>
    /// Gets or sets the box shadow for step content.
    /// </summary>
    public string? ContentShadow { get; set; }
    /// <summary>
    /// Gets or sets the focus outline style.
    /// </summary>
    public string? FocusOutline { get; set; }
    /// <summary>
    /// Gets or sets the text color for success step markers.
    /// </summary>
    public string? MarkerSuccessColor { get; set; }
}

