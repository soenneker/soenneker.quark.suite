using Soenneker.Extensions.String;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap alert utilities.
/// </summary>
/// <summary>
/// Builder for Bootstrap alert utilities.
/// </summary>
public sealed class AlertStyleBuilder : ICssBuilder
{
    private AlertStyle? _alert;

    /// <summary>
    /// Gets a value indicating whether this builder is empty (no alert style is set).
    /// </summary>
    public bool IsEmpty => !_alert.HasValue;

    /// <summary>
    /// Gets a value indicating whether this builder generates CSS classes.
    /// </summary>
    public bool IsCssClass => true;

    /// <summary>
    /// Gets a value indicating whether this builder generates inline CSS styles.
    /// </summary>
    public bool IsCssStyle => false;

    /// <summary>
    /// Sets the alert style to the specified value.
    /// </summary>
    /// <param name="alert">The alert style to set.</param>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Set(AlertStyle alert)
    {
        _alert = alert;
        return this;
    }

    // Alert types
    /// <summary>
    /// Sets the alert style to primary.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Primary()
    {
        _alert = AlertStyle.Primary;
        return this;
    }

    /// <summary>
    /// Sets the alert style to secondary.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Secondary()
    {
        _alert = AlertStyle.Secondary;
        return this;
    }

    /// <summary>
    /// Sets the alert style to success.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Success()
    {
        _alert = AlertStyle.Success;
        return this;
    }

    /// <summary>
    /// Sets the alert style to danger.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Danger()
    {
        _alert = AlertStyle.Danger;
        return this;
    }

    /// <summary>
    /// Sets the alert style to warning.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Warning()
    {
        _alert = AlertStyle.Warning;
        return this;
    }

    /// <summary>
    /// Sets the alert style to info.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Info()
    {
        _alert = AlertStyle.Info;
        return this;
    }

    /// <summary>
    /// Sets the alert style to light.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Light()
    {
        _alert = AlertStyle.Light;
        return this;
    }

    /// <summary>
    /// Sets the alert style to dark.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder Dark()
    {
        _alert = AlertStyle.Dark;
        return this;
    }

    // Dismissible variants
    /// <summary>
    /// Sets the alert style to primary with dismissible functionality.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder PrimaryDismissible()
    {
        _alert = AlertStyle.PrimaryDismissible;
        return this;
    }

    /// <summary>
    /// Sets the alert style to secondary with dismissible functionality.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder SecondaryDismissible()
    {
        _alert = AlertStyle.SecondaryDismissible;
        return this;
    }

    /// <summary>
    /// Sets the alert style to success with dismissible functionality.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder SuccessDismissible()
    {
        _alert = AlertStyle.SuccessDismissible;
        return this;
    }

    /// <summary>
    /// Sets the alert style to danger with dismissible functionality.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder DangerDismissible()
    {
        _alert = AlertStyle.DangerDismissible;
        return this;
    }

    /// <summary>
    /// Sets the alert style to warning with dismissible functionality.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder WarningDismissible()
    {
        _alert = AlertStyle.WarningDismissible;
        return this;
    }

    /// <summary>
    /// Sets the alert style to info with dismissible functionality.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder InfoDismissible()
    {
        _alert = AlertStyle.InfoDismissible;
        return this;
    }

    /// <summary>
    /// Sets the alert style to light with dismissible functionality.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder LightDismissible()
    {
        _alert = AlertStyle.LightDismissible;
        return this;
    }

    /// <summary>
    /// Sets the alert style to dark with dismissible functionality.
    /// </summary>
    /// <returns>This builder instance for method chaining.</returns>
    public AlertStyleBuilder DarkDismissible()
    {
        _alert = AlertStyle.DarkDismissible;
        return this;
    }

    /// <summary>
    /// Returns the CSS class string representation of the alert style.
    /// </summary>
    /// <returns>The CSS class string, or an empty string if no alert style is set.</returns>
    public override string ToString()
    {
        if (!_alert.HasValue)
            return string.Empty;
        return GetAlertClass(_alert.Value);
    }

    private static string GetAlertClass(AlertStyle alert)
    {
        var baseClass = alert.Type.Value switch
        {
            AlertType.PrimaryValue => "alert-primary",
            AlertType.SecondaryValue => "alert-secondary",
            AlertType.SuccessValue => "alert-success",
            AlertType.DangerValue => "alert-danger",
            AlertType.WarningValue => "alert-warning",
            AlertType.InfoValue => "alert-info",
            AlertType.LightValue => "alert-light",
            AlertType.DarkValue => "alert-dark",
            _ => string.Empty
        };

        if (baseClass.IsNullOrEmpty())
            return string.Empty;

        var dismissibleClass = alert.Dismissible.Value switch
        {
            AlertDismissibleType.DismissibleValue => " alert-dismissible",
            _ => string.Empty
        };

        return $"{baseClass}{dismissibleClass}";
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public string ToClass() => ToString();

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    /// <returns>An empty string, as this builder only generates CSS classes.</returns>
    public string ToStyle() => string.Empty;
}