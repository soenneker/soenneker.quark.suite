namespace Soenneker.Quark;

/// <summary>
/// Defines the visual style variant for an Alert component (shadcn/blueprint style).
/// </summary>
/// <remarks>
/// Each variant uses CSS custom properties for theming (e.g. --alert-success, --alert-info).
/// </remarks>
public enum AlertVariant
{
    /// <summary>
    /// Default alert style with neutral muted accent. General informational messages.
    /// </summary>
    Default,

    /// <summary>
    /// Success alert style with green accent. Successful operations or positive status.
    /// </summary>
    Success,

    /// <summary>
    /// Info alert style with blue accent. Informational or educational messages.
    /// </summary>
    Info,

    /// <summary>
    /// Warning alert style with amber/orange accent. Caution or potential issues.
    /// </summary>
    Warning,

    /// <summary>
    /// Danger alert style with red accent. Errors, critical warnings, or destructive actions.
    /// </summary>
    Danger
}
