using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap nav utilities.
/// </summary>
public class NavStyleBuilder : ICssBuilder
{
    private NavStyle? _nav;

    /// <summary>
    /// Gets a value indicating whether the builder is empty (no nav style set).
    /// </summary>
    public bool IsEmpty => !_nav.HasValue;
    /// <summary>
    /// Gets a value indicating whether this builder generates CSS classes (always true).
    /// </summary>
    public bool IsCssClass => true;
    /// <summary>
    /// Gets a value indicating whether this builder generates inline styles (always false).
    /// </summary>
    public bool IsCssStyle => false;

    /// <summary>
    /// Sets the nav style value.
    /// </summary>
    /// <param name="nav">The nav style value to set.</param>
    /// <returns>The builder instance for method chaining.</returns>
    public NavStyleBuilder Set(NavStyle nav)
    {
        _nav = nav;
        return this;
    }

    // Nav styles
    /// <summary>
    /// Sets the nav style to default.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public NavStyleBuilder Default()
    {
        _nav = NavStyle.Default;
        return this;
    }

    /// <summary>
    /// Sets the nav style to tabs.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public NavStyleBuilder Tabs()
    {
        _nav = NavStyle.Tabs;
        return this;
    }

    /// <summary>
    /// Sets the nav style to pills.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public NavStyleBuilder Pills()
    {
        _nav = NavStyle.Pills;
        return this;
    }

    /// <summary>
    /// Sets the nav style to justified.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public NavStyleBuilder Justified()
    {
        _nav = NavStyle.Justified;
        return this;
    }

    /// <summary>
    /// Sets the nav style to fill.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public NavStyleBuilder Fill()
    {
        _nav = NavStyle.Fill;
        return this;
    }

    /// <summary>
    /// Returns the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string, or an empty string if no nav style is set.</returns>
    public override string ToString()
    {
        if (!_nav.HasValue) return string.Empty;
        return GetNavClass(_nav.Value);
    }

    private static string GetNavClass(NavStyle nav)
    {
        var baseClass = "nav";
        string styleClass = nav.Type.Value switch
        {
            NavStyleType.TabsValue => " nav-tabs",
            NavStyleType.PillsValue => " nav-pills",
            NavStyleType.JustifiedValue => " nav-justified",
            NavStyleType.FillValue => " nav-fill",
            _ => string.Empty
        };

        return $"{baseClass}{styleClass}";
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string, or an empty string if no nav style is set.</returns>
    public string ToClass() => ToString();
    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// Nav styles are typically handled via Bootstrap classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as nav styles are handled via classes.</returns>
    public string ToStyle() => string.Empty;
}
