using Intellenum;

namespace Soenneker.Quark;

[Intellenum<string>]
public sealed partial class IconSize
{
    public static readonly IconSize None = new("");

    public static readonly IconSize ExtraSmall = new("fa-xs");
    public static readonly IconSize Small = new("fa-sm");
    public static readonly IconSize Large = new("fa-lg");

    public static readonly IconSize TwoX = new("fa-2x");
    public static readonly IconSize ThreeX = new("fa-3x");
    public static readonly IconSize FourX = new("fa-4x");
    public static readonly IconSize FiveX = new("fa-5x");
    public static readonly IconSize SixX = new("fa-6x");
    public static readonly IconSize SevenX = new("fa-7x");
    public static readonly IconSize EightX = new("fa-8x");
    public static readonly IconSize NineX = new("fa-9x");
    public static readonly IconSize TenX = new("fa-10x");
}
