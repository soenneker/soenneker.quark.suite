namespace Soenneker.Quark;

/// <summary>
/// Represents the image context record.
/// </summary>
/// <param name="Source">The source.</param>
/// <param name="Alt">The alt.</param>
/// <param name="Loading">The loading.</param>
/// <param name="Decoding">The decoding.</param>
/// <param name="SrcSet">The src set.</param>
/// <param name="Sizes">The sizes.</param>
public sealed record ImageContext(string? Source, string? Alt, string? Loading, string? Decoding, string? SrcSet, string? Sizes)
{
    /// <summary>HTML image width in pixels.</summary>
    public int? IntrinsicWidth { get; init; }

    /// <summary>HTML image height in pixels.</summary>
    public int? IntrinsicHeight { get; init; }
}
