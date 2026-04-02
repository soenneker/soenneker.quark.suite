using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents dismissible alert behaviors.
/// </summary>
[EnumValue<string>]
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
