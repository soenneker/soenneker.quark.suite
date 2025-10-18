namespace Soenneker.Quark.Components.Progresses.Abstract;

/// <summary>
/// Represents the inner progress bar that displays the actual progress value.
/// </summary>
public interface IProgressBar : IElement
{
    /// <summary>
    /// Gets or sets the progress value (0-100).
    /// </summary>
    int Value { get; set; }


    /// <summary>
    /// Gets or sets whether the progress bar should have striped styling.
    /// </summary>
    bool Striped { get; set; }

    /// <summary>
    /// Gets or sets whether the progress bar should be animated.
    /// </summary>
    bool Animated { get; set; }

    /// <summary>
    /// Gets or sets whether the progress percentage label should be displayed.
    /// </summary>
    bool ShowLabel { get; set; }
}

