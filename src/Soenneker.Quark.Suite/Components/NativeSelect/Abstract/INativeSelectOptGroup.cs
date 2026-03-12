namespace Soenneker.Quark;

/// <summary>
/// Represents an option group within <see cref="NativeSelect"/>.
/// </summary>
public interface INativeSelectOptGroup : IElement
{
    /// <summary>
    /// Gets or sets the group label.
    /// </summary>
    string? Label { get; set; }
}
