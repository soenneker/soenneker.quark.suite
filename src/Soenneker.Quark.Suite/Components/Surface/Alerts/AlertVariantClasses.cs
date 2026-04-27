namespace Soenneker.Quark;

public static class AlertVariantClasses
{
    /// <summary>
    /// Gets the variant-specific Tailwind classes for the given variant and accent border option.
    /// </summary>
    public static string GetVariantClasses(AlertVariant variant, bool accentBorder)
    {
        if (ReferenceEquals(variant, AlertVariant.Destructive) || string.Equals(variant?.Value, "danger", System.StringComparison.Ordinal))
            return accentBorder
                ? "border-l-alert-danger bg-alert-danger-bg border-alert-danger/30 [&>svg]:text-alert-danger"
                : "border-alert-danger/30 bg-alert-danger-bg [&>svg]:text-alert-danger";

        return "bg-muted/30 [&>svg]:text-muted-foreground";
    }
}
