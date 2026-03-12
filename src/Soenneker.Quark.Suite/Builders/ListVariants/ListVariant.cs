using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Bootstrap list variant classes with fluent API.
/// </summary>
public static class ListVariant
{
    /// <summary>
    /// Removes default list styling (bullets, padding) - Bootstrap .list-unstyled
    /// </summary>
    public static ListVariantBuilder Unstyled => new(ListVariantType.Unstyled);

    /// <summary>
    /// Makes list items inline - Bootstrap .list-inline
    /// </summary>
    public static ListVariantBuilder Inline => new(ListVariantType.Inline);

    /// <summary>
    /// For items within an inline list - Bootstrap .list-inline-item
    /// </summary>
    public static ListVariantBuilder InlineItem => new(ListVariantType.InlineItem);
}

