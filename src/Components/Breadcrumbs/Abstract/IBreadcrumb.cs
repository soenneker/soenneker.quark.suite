namespace Soenneker.Quark;

/// <summary>
/// Represents a breadcrumb navigation component.
/// </summary>
public interface IBreadcrumb : IElement
{
    /// <summary>
    /// Gets or sets the color scheme of the breadcrumb.
    /// </summary>
    CssValue<ColorBuilder> Color { get; set; }

    /// <summary>
    /// Gets or sets whether the breadcrumb should use a large size.
    /// </summary>
    bool Large { get; set; }
}

