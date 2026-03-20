namespace Soenneker.Quark;

public static class AlertVariantClasses
{
    /// <summary>
    /// Gets the variant-specific Tailwind classes for the given variant and accent border option.
    /// </summary>
    public static string GetVariantClasses(AlertVariant variant, bool accentBorder)
    {
        return variant switch
        {
            AlertVariant.Default => "bg-muted/30 [&>svg]:text-muted-foreground",
            AlertVariant.Success => accentBorder
                ? "border-l-alert-success bg-alert-success-bg border-alert-success/30 [&>svg]:text-alert-success"
                : "border-alert-success/30 bg-alert-success-bg [&>svg]:text-alert-success",
            AlertVariant.Info => accentBorder
                ? "border-l-alert-info bg-alert-info-bg border-alert-info/30 [&>svg]:text-alert-info"
                : "border-alert-info/30 bg-alert-info-bg [&>svg]:text-alert-info",
            AlertVariant.Warning => accentBorder
                ? "border-l-alert-warning bg-alert-warning-bg border-alert-warning/30 [&>svg]:text-alert-warning"
                : "border-alert-warning/30 bg-alert-warning-bg [&>svg]:text-alert-warning",
            AlertVariant.Danger => accentBorder
                ? "border-l-alert-danger bg-alert-danger-bg border-alert-danger/30 [&>svg]:text-alert-danger"
                : "border-alert-danger/30 bg-alert-danger-bg [&>svg]:text-alert-danger",
            _ => "bg-muted/30 [&>svg]:text-muted-foreground"
        };
    }
}
