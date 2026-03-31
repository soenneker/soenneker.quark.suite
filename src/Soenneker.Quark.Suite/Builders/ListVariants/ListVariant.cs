namespace Soenneker.Quark;

/// <summary>
/// Tailwind-aligned list layout variants.
/// </summary>
public static class ListVariant
{
    /// <summary>
    /// Removes default list styling.
    /// </summary>
    public static ListVariantBuilder None => new(ListVariantType.None);

    /// <summary>
    /// Applies inline layout utilities to the list.
    /// </summary>
    public static ListVariantBuilder Inline => new(ListVariantType.Inline);

    /// <summary>
    /// Applies inline item utilities to the list item.
    /// </summary>
    public static ListVariantBuilder InlineItem => new(ListVariantType.InlineItem);
}

