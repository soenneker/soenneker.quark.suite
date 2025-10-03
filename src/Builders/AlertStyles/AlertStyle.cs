using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap alert utilities.
/// </summary>
public readonly struct AlertStyle
{
    public readonly AlertType Type;
    public readonly AlertDismissibleType Dismissible;

    public AlertStyle(AlertType type, AlertDismissibleType? dismissible = null)
    {
        Type = type;
        Dismissible = dismissible ?? AlertDismissibleType.None;
    }

    public static AlertStyle Primary => new(AlertType.Primary);
    public static AlertStyle Secondary => new(AlertType.Secondary);
    public static AlertStyle Success => new(AlertType.Success);
    public static AlertStyle Danger => new(AlertType.Danger);
    public static AlertStyle Warning => new(AlertType.Warning);
    public static AlertStyle Info => new(AlertType.Info);
    public static AlertStyle Light => new(AlertType.Light);
    public static AlertStyle Dark => new(AlertType.Dark);

    public static AlertStyle PrimaryDismissible => new(AlertType.Primary, AlertDismissibleType.Dismissible);
    public static AlertStyle SecondaryDismissible => new(AlertType.Secondary, AlertDismissibleType.Dismissible);
    public static AlertStyle SuccessDismissible => new(AlertType.Success, AlertDismissibleType.Dismissible);
    public static AlertStyle DangerDismissible => new(AlertType.Danger, AlertDismissibleType.Dismissible);
    public static AlertStyle WarningDismissible => new(AlertType.Warning, AlertDismissibleType.Dismissible);
    public static AlertStyle InfoDismissible => new(AlertType.Info, AlertDismissibleType.Dismissible);
    public static AlertStyle LightDismissible => new(AlertType.Light, AlertDismissibleType.Dismissible);
    public static AlertStyle DarkDismissible => new(AlertType.Dark, AlertDismissibleType.Dismissible);
}
