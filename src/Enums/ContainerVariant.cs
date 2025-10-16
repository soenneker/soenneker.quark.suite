using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the variant of container.
/// </summary>
[Intellenum<string>]
public sealed partial class ContainerVariant
{
    public static readonly ContainerVariant Default = new("default");
    public static readonly ContainerVariant Fluid = new("fluid");
    public static readonly ContainerVariant Responsive = new("responsive");
}