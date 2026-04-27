using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines size options for <see cref="NativeSelect"/>.
/// </summary>
[EnumValue]
public sealed partial class NativeSelectSize
{
    /// <summary>
    /// Default height.
    /// </summary>
    public static readonly NativeSelectSize Default = new(0);

    /// <summary>
    /// Small height.
    /// </summary>
    public static readonly NativeSelectSize Sm = new(1);
}
