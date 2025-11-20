using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents icon family/style. Maps to Font Awesome 6 style classes.
/// </summary>
[Intellenum<string>]
public sealed partial class IconStyle
{
    /// <summary>
    /// Solid style (filled icons).
    /// </summary>
    public static readonly IconStyle Solid = new("fa-solid");

    /// <summary>
    /// Regular style (outlined icons).
    /// </summary>
    public static readonly IconStyle Regular = new("fa-regular");

    /// <summary>
    /// Light style (lighter weight icons).
    /// </summary>
    public static readonly IconStyle Light = new("fa-light");

    /// <summary>
    /// Thin style (thinnest weight icons).
    /// </summary>
    public static readonly IconStyle Thin = new("fa-thin");

    /// <summary>
    /// Duotone style (two-tone icons).
    /// </summary>
    public static readonly IconStyle Duotone = new("fa-duotone");

    /// <summary>
    /// Brands style (brand/logo icons).
    /// </summary>
    public static readonly IconStyle Brands = new("fa-brands");

    /// <summary>
    /// Sharp solid style (sharp geometry with solid fill).
    /// </summary>
    public static readonly IconStyle SharpSolid = new("fa-sharp fa-solid");

    /// <summary>
    /// Sharp regular style (sharp geometry with regular outline).
    /// </summary>
    public static readonly IconStyle SharpRegular = new("fa-sharp fa-regular");

    /// <summary>
    /// Sharp light style (sharp geometry with light weight).
    /// </summary>
    public static readonly IconStyle SharpLight = new("fa-sharp fa-light");

    /// <summary>
    /// Sharp thin style (sharp geometry with thin weight).
    /// </summary>
    public static readonly IconStyle SharpThin = new("fa-sharp fa-thin");

    /// <summary>
    /// Sharp duotone style (sharp geometry with duotone fill).
    /// </summary>
    public static readonly IconStyle SharpDuotone = new("fa-sharp-duotone");
}
