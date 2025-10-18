namespace Soenneker.Quark;

/// <summary>
/// Represents an individual step within a steps wizard.
/// </summary>
public interface IStep : IElement
{
    /// <summary>
    /// Gets or sets the unique name identifier for this step.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the display index of the step.
    /// </summary>
    int? Index { get; set; }

    /// <summary>
    /// Gets or sets whether the step is completed.
    /// </summary>
    bool Completed { get; set; }

    /// <summary>
    /// Gets or sets whether the step is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the background color scheme of the step.
    /// </summary>
    CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }
}

