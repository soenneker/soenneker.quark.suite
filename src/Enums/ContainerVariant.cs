using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the variant of container.
/// </summary>
[Intellenum<string>]
public sealed partial class ContainerVariant
{
    /// <summary>
    /// Default container variant.
    /// </summary>
    public static readonly ContainerVariant Default = new("default");

    /// <summary>
    /// Fluid container variant (full width).
    /// </summary>
    public static readonly ContainerVariant Fluid = new("fluid");

    /// <summary>
    /// Responsive container variant.
    /// </summary>
    public static readonly ContainerVariant Responsive = new("responsive");
}