using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents Bootstrap alert dismissible types.
/// </summary>
[Intellenum<string>]
public sealed partial class AlertDismissibleType
{
    /// <summary>
    /// No dismissible behavior.
    /// </summary>
    public static readonly AlertDismissibleType None = new("");

    /// <summary>
    /// Dismissible alert.
    /// </summary>
    public static readonly AlertDismissibleType Dismissible = new("dismissible");
}
