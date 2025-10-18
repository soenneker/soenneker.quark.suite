namespace Soenneker.Quark;

/// <summary>
/// Represents a navigation element for containing navigation links.
/// </summary>
public interface INav : IElement
{
    /// <summary>
    /// Gets or sets the nav styling configuration.
    /// </summary>
    CssValue<NavStyleBuilder>? NavStyle { get; set; }
}