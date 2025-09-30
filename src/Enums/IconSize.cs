using Intellenum;

namespace Soenneker.Quark.Enums;

[Intellenum<string>]
public sealed partial class IconSize
{
    public static readonly IconSize None = new("");

    public static readonly IconSize ExtraSmall = new("fa-xs");
    public static readonly IconSize Small = new("fa-sm");
    public static readonly IconSize Large = new("fa-lg");

    public static readonly IconSize IsTwoX = new("fa-2x");
    public static readonly IconSize IsThreeX = new("fa-3x");
    public static readonly IconSize IsFourX = new("fa-4x");
    public static readonly IconSize IsFiveX = new("fa-5x");
    public static readonly IconSize IsSixX = new("fa-6x");
    public static readonly IconSize IsSevenX = new("fa-7x");
    public static readonly IconSize IsEightX = new("fa-8x");
    public static readonly IconSize IsNineX = new("fa-9x");
    public static readonly IconSize IsTenX = new("fa-10x");
}
