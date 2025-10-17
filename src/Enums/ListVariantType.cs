using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the Bootstrap list variant types.
/// </summary>
[Intellenum<string>]
public sealed partial class ListVariantType
{
    /// <summary>
    /// Removes default list styling - Bootstrap .list-unstyled
    /// </summary>
    public static readonly ListVariantType Unstyled = new("unstyled");

    /// <summary>
    /// Makes list items inline - Bootstrap .list-inline
    /// </summary>
    public static readonly ListVariantType Inline = new("inline");

    /// <summary>
    /// For items within an inline list - Bootstrap .list-inline-item
    /// </summary>
    public static readonly ListVariantType InlineItem = new("inline-item");
}

