using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the interaction mode for list group items.
/// </summary>
[Intellenum<string>]
public partial class ListGroupMode
{
    /// <summary>
    /// Static mode - items are not selectable or interactive.
    /// </summary>
    public static readonly ListGroupMode Static = new("static");

    /// <summary>
    /// Selectable mode - items can be selected by clicking.
    /// </summary>
    public static readonly ListGroupMode Selectable = new("selectable");
}
