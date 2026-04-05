using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the visual style variant for an Alert component (shadcn style).
/// </summary>
/// <remarks>
/// Each variant uses CSS custom properties for theming (e.g. --alert-success, --alert-info).
/// </remarks>
[EnumValue<string>]
public sealed partial class AlertVariant
{
    /// <summary>
    /// Default alert style with neutral muted accent. General informational messages.
    /// </summary>
    public static readonly AlertVariant Default = new("default");

    /// <summary>
    /// Success alert style with green accent. Successful operations or positive status.
    /// </summary>
    public static readonly AlertVariant Success = new("success");

    /// <summary>
    /// Info alert style with blue accent. Informational or educational messages.
    /// </summary>
    public static readonly AlertVariant Info = new("info");

    /// <summary>
    /// Warning alert style with amber/orange accent. Caution or potential issues.
    /// </summary>
    public static readonly AlertVariant Warning = new("warning");

    /// <summary>
    /// Danger alert style with red accent. Errors, critical warnings, or destructive actions.
    /// </summary>
    public static readonly AlertVariant Danger = new("danger");
}
