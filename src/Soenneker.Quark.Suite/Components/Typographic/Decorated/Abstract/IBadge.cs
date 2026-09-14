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
    /// <summary>Optional semantic tone. Explicit color parameters and presets override its colors.</summary>
    SemanticTone? Tone { get; set; }

    /// <summary>Whether the child receives the badge attributes instead of a wrapping span.</summary>
    bool AsChild { get; set; }
}
