using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the selection mode for list group items.
/// </summary>
[Intellenum<string>]
public partial class ListGroupSelectionMode
{
    /// <summary>
    /// Single selection mode - only one item can be selected at a time.
    /// </summary>
    public static readonly ListGroupSelectionMode Single = new("single");

    /// <summary>
    /// Multiple selection mode - multiple items can be selected simultaneously.
    /// </summary>
    public static readonly ListGroupSelectionMode Multiple = new("multiple");
}
