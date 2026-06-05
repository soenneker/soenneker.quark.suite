namespace Soenneker.Quark;

/// <summary>
/// Defines the item media contract.
/// </summary>
public interface IItemMedia : IElement
{
    /// <summary>
    /// Gets or sets variant.
    /// </summary>
    ItemMediaVariant Variant { get; set; }
}
