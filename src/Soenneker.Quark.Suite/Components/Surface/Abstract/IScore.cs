namespace Soenneker.Quark;

/// <summary>
/// Represents a circular score indicator with a percentage fill.
/// </summary>
public interface IScore : IElement
{
    /// <summary>
    /// Gets or sets the score value (0-100).
    /// </summary>
    int Value { get; set; }

    /// <summary>
    /// Gets or sets the text displayed beneath the score.
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// Gets or sets the diameter of the score circle, in pixels.
    /// </summary>
    int Size { get; set; }

    /// <summary>
    /// Gets or sets the thickness of the score ring, in pixels.
    /// </summary>
    int Thickness { get; set; }
}
