using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents Tailwind container variants.
/// </summary>
[EnumValue<string>]
public sealed partial class ContainerVariant
{
    /// <summary>
    /// Tailwind's default container utility.
    /// </summary>
    public static readonly ContainerVariant Default = new("default");
}