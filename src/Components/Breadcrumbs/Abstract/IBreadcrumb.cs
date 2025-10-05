namespace Soenneker.Quark;

/// <summary>
/// Represents a breadcrumb navigation component.
/// </summary>
public interface IBreadcrumb : IElement
{
    /// <summary>
    /// Gets or sets whether the breadcrumb should use a large size.
    /// </summary>
    bool Large { get; set; }

    /// <summary>
    /// Gets or sets whether the breadcrumb should use a small size.
    /// </summary>
    bool Small { get; set; }

    /// <summary>
    /// Gets or sets whether the breadcrumb should show dividers.
    /// </summary>
    bool Divider { get; set; }

    /// <summary>
    /// Gets or sets the divider text for the breadcrumb.
    /// </summary>
    string? DividerText { get; set; }
}

