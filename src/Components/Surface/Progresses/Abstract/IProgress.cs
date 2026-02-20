namespace Soenneker.Quark;

/// <summary>
/// Represents a progress component for displaying task completion.
/// </summary>
public interface IProgress : IElement
{
    /// <summary>
    /// Gets or sets the current progress value (0–100).
    /// </summary>
    int Value { get; set; }

    /// <summary>
    /// Gets or sets the maximum value for ARIA semantics.
    /// </summary>
    int Max { get; set; }
}
