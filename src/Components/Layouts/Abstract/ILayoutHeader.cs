namespace Soenneker.Quark;

/// <summary>
/// Represents the header section of a layout.
/// </summary>
public interface ILayoutHeader : IElement
{
    /// <summary>
    /// Gets or sets whether the header should be fixed to the top.
    /// </summary>
    bool Fixed { get; set; }
}

