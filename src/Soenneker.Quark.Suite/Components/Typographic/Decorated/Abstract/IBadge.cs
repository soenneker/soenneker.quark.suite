namespace Soenneker.Quark;

/// <summary>
/// Represents a badge component for displaying labels or indicators (shadcn/ui).
/// </summary>
public interface IBadge : IElement
{
    /// <summary>
    /// Gets or sets the visual style variant (shadcn/ui).
    /// </summary>
    BadgeVariant Variant { get; set; }
}