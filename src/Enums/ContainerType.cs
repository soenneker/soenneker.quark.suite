using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the type of container.
/// </summary>
[Intellenum<string>]
public sealed partial class ContainerType
{
    public static readonly ContainerType Default = new("default");
    public static readonly ContainerType Fluid = new("fluid");
    public static readonly ContainerType Responsive = new("responsive");
}
