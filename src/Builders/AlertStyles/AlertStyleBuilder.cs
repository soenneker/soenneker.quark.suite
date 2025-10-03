using Soenneker.Extensions.String;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap alert utilities.
/// </summary>
public class AlertStyleBuilder : ICssBuilder
{
    private AlertStyle? _alert;

    public bool IsEmpty => !_alert.HasValue;
    public bool IsCssClass => true;
    public bool IsCssStyle => false;

    public AlertStyleBuilder Set(AlertStyle alert)
    {
        _alert = alert;
        return this;
    }

    // Alert types
    public AlertStyleBuilder Primary()
    {
        _alert = AlertStyle.Primary;
        return this;
    }

    public AlertStyleBuilder Secondary()
    {
        _alert = AlertStyle.Secondary;
        return this;
    }

    public AlertStyleBuilder Success()
    {
        _alert = AlertStyle.Success;
        return this;
    }

    public AlertStyleBuilder Danger()
    {
        _alert = AlertStyle.Danger;
        return this;
    }

    public AlertStyleBuilder Warning()
    {
        _alert = AlertStyle.Warning;
        return this;
    }

    public AlertStyleBuilder Info()
    {
        _alert = AlertStyle.Info;
        return this;
    }

    public AlertStyleBuilder Light()
    {
        _alert = AlertStyle.Light;
        return this;
    }

    public AlertStyleBuilder Dark()
    {
        _alert = AlertStyle.Dark;
        return this;
    }

    // Dismissible variants
    public AlertStyleBuilder PrimaryDismissible()
    {
        _alert = AlertStyle.PrimaryDismissible;
        return this;
    }

    public AlertStyleBuilder SecondaryDismissible()
    {
        _alert = AlertStyle.SecondaryDismissible;
        return this;
    }

    public AlertStyleBuilder SuccessDismissible()
    {
        _alert = AlertStyle.SuccessDismissible;
        return this;
    }

    public AlertStyleBuilder DangerDismissible()
    {
        _alert = AlertStyle.DangerDismissible;
        return this;
    }

    public AlertStyleBuilder WarningDismissible()
    {
        _alert = AlertStyle.WarningDismissible;
        return this;
    }

    public AlertStyleBuilder InfoDismissible()
    {
        _alert = AlertStyle.InfoDismissible;
        return this;
    }

    public AlertStyleBuilder LightDismissible()
    {
        _alert = AlertStyle.LightDismissible;
        return this;
    }

    public AlertStyleBuilder DarkDismissible()
    {
        _alert = AlertStyle.DarkDismissible;
        return this;
    }

    public override string ToString()
    {
        if (!_alert.HasValue) return string.Empty;
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

        if (baseClass.IsNullOrEmpty()) return string.Empty;

        var dismissibleClass = alert.Dismissible.Value switch
        {
            AlertDismissibleType.DismissibleValue => " alert-dismissible",
            _ => string.Empty
        };

        return $"{baseClass}{dismissibleClass}";
    }

    public string ToClass() => ToString();
    public string ToStyle() => string.Empty;
}
