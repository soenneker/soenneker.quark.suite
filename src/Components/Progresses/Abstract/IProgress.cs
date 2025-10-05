namespace Soenneker.Quark;

/// <summary>
/// Represents a progress bar container component for displaying task completion.
/// </summary>
public interface IProgress : IElement
{
    /// <summary>
    /// Gets or sets the background color scheme of the progress bar.
    /// </summary>
    CssValue<ColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the size of the progress bar.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets whether the progress bar should have striped styling.
    /// </summary>
    bool Striped { get; set; }

    /// <summary>
    /// Gets or sets whether the progress bar should be animated.
    /// </summary>
    bool Animated { get; set; }
}

