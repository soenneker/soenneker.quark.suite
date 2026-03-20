namespace Soenneker.Quark;

/// <summary>
/// Represents the media container in an empty-state layout.
/// </summary>
public interface IEmptyMedia : IElement
{
    /// <summary>
    /// Gets or sets the visual style variant.
    /// </summary>
    EmptyMediaVariant Variant { get; set; }
}
