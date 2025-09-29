using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Sizes;

/// <summary>
/// Size utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Size
{
    /// <summary>
    /// Default size (no size modifier).
    /// </summary>
    public static SizeBuilder Default => new(SizeType.Default);

    /// <summary>
    /// Extra small size.
    /// </summary>
    public static SizeBuilder ExtraSmall => new(SizeType.ExtraSmall);

    /// <summary>
    /// Small size.
    /// </summary>
    public static SizeBuilder Small => new(SizeType.Small);

    /// <summary>
    /// Medium size.
    /// </summary>
    public static SizeBuilder Medium => new(SizeType.Medium);

    /// <summary>
    /// Large size.
    /// </summary>
    public static SizeBuilder Large => new(SizeType.Large);

    /// <summary>
    /// Extra large size.
    /// </summary>
    public static SizeBuilder ExtraLarge => new(SizeType.ExtraLarge);

    /// <summary>
    /// Extra extra large size.
    /// </summary>
    public static SizeBuilder ExtraExtraLarge => new(SizeType.ExtraExtraLarge);

    /// <summary>
    /// Create from a SizeType enum value.
    /// </summary>
    public static SizeBuilder From(SizeType sizeType) => new(sizeType);
}
