using Intellenum;

namespace Soenneker.Quark;

/// <summary>
/// Represents icon family/style. Maps to Font Awesome 6 style classes.
/// </summary>
[Intellenum<string>]
public sealed partial class IconStyle
{
    public static readonly IconStyle Solid = new("fa-solid");
    public static readonly IconStyle Regular = new("fa-regular");
    public static readonly IconStyle Light = new("fa-light");
    public static readonly IconStyle Thin = new("fa-thin");
    public static readonly IconStyle Duotone = new("fa-duotone");
    public static readonly IconStyle Brands = new("fa-brands");
    public static readonly IconStyle SharpSolid = new("fa-sharp fa-solid");
    public static readonly IconStyle SharpRegular = new("fa-sharp fa-regular");
    public static readonly IconStyle SharpLight = new("fa-sharp fa-light");
    public static readonly IconStyle SharpThin = new("fa-sharp fa-thin");
    public static readonly IconStyle SharpDuotone = new("fa-sharp-duotone");
}
