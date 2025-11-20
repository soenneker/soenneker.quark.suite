using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap alert utilities.
/// </summary>
public readonly struct AlertStyle
{
    /// <summary>
    /// Gets the alert type.
    /// </summary>
    public readonly AlertType Type;

    /// <summary>
    /// Gets the dismissible type for the alert.
    /// </summary>
    public readonly AlertDismissibleType Dismissible;

    /// <summary>
    /// Initializes a new instance of the AlertStyle struct.
    /// </summary>
    /// <param name="type">The alert type.</param>
    /// <param name="dismissible">The dismissible type. If null, defaults to None.</param>
    public AlertStyle(AlertType type, AlertDismissibleType? dismissible = null)
    {
        Type = type;
        Dismissible = dismissible ?? AlertDismissibleType.None;
    }

    /// <summary>
    /// Gets a primary alert style.
    /// </summary>
    public static AlertStyle Primary => new(AlertType.Primary);

    /// <summary>
    /// Gets a secondary alert style.
    /// </summary>
    public static AlertStyle Secondary => new(AlertType.Secondary);

    /// <summary>
    /// Gets a success alert style.
    /// </summary>
    public static AlertStyle Success => new(AlertType.Success);

    /// <summary>
    /// Gets a danger alert style.
    /// </summary>
    public static AlertStyle Danger => new(AlertType.Danger);

    /// <summary>
    /// Gets a warning alert style.
    /// </summary>
    public static AlertStyle Warning => new(AlertType.Warning);

    /// <summary>
    /// Gets an info alert style.
    /// </summary>
    public static AlertStyle Info => new(AlertType.Info);

    /// <summary>
    /// Gets a light alert style.
    /// </summary>
    public static AlertStyle Light => new(AlertType.Light);

    /// <summary>
    /// Gets a dark alert style.
    /// </summary>
    public static AlertStyle Dark => new(AlertType.Dark);

    /// <summary>
    /// Gets a primary alert style with dismissible functionality.
    /// </summary>
    public static AlertStyle PrimaryDismissible => new(AlertType.Primary, AlertDismissibleType.Dismissible);

    /// <summary>
    /// Gets a secondary alert style with dismissible functionality.
    /// </summary>
    public static AlertStyle SecondaryDismissible => new(AlertType.Secondary, AlertDismissibleType.Dismissible);

    /// <summary>
    /// Gets a success alert style with dismissible functionality.
    /// </summary>
    public static AlertStyle SuccessDismissible => new(AlertType.Success, AlertDismissibleType.Dismissible);

    /// <summary>
    /// Gets a danger alert style with dismissible functionality.
    /// </summary>
    public static AlertStyle DangerDismissible => new(AlertType.Danger, AlertDismissibleType.Dismissible);

    /// <summary>
    /// Gets a warning alert style with dismissible functionality.
    /// </summary>
    public static AlertStyle WarningDismissible => new(AlertType.Warning, AlertDismissibleType.Dismissible);

    /// <summary>
    /// Gets an info alert style with dismissible functionality.
    /// </summary>
    public static AlertStyle InfoDismissible => new(AlertType.Info, AlertDismissibleType.Dismissible);

    /// <summary>
    /// Gets a light alert style with dismissible functionality.
    /// </summary>
    public static AlertStyle LightDismissible => new(AlertType.Light, AlertDismissibleType.Dismissible);

    /// <summary>
    /// Gets a dark alert style with dismissible functionality.
    /// </summary>
    public static AlertStyle DarkDismissible => new(AlertType.Dark, AlertDismissibleType.Dismissible);
}
