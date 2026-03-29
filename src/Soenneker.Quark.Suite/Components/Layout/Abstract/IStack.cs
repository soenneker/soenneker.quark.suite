using Soenneker.Quark.Layout;

namespace Soenneker.Quark;

/// <summary>
/// Represents a layout container that renders with flex stack defaults.
/// </summary>
public interface IStack : IElement
{
    /// <summary>
    /// Gets or sets the direction of the stack.
    /// </summary>
    StackOrientation Orientation { get; set; }

    /// <summary>
    /// Gets or sets whether the stack should render as inline-flex.
    /// </summary>
    bool Inline { get; set; }
}
