using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines size options for <see cref="NativeSelect"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class NativeSelectSize
{
    /// <summary>
    /// Default height.
    /// </summary>
    public static readonly NativeSelectSize Default = new("default");

    /// <summary>
    /// Small height.
    /// </summary>
    public static readonly NativeSelectSize Sm = new("sm");
}
