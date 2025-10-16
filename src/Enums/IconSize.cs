using Intellenum;

namespace Soenneker.Quark.Enums;

[Intellenum<string>]
public sealed partial class IconSize
{
    public static readonly IconSize None = new("");

    public static readonly IconSize ExtraSmall = new("fa-xs");
    public static readonly IconSize Small = new("fa-sm");
    public static readonly IconSize Large = new("fa-lg");

    public static readonly IconSize Is2x = new("fa-2x");
    public static readonly IconSize Is3x = new("fa-3x");
    public static readonly IconSize Is4x = new("fa-4x");
    public static readonly IconSize Is5x = new("fa-5x");
    public static readonly IconSize Is6x = new("fa-6x");
    public static readonly IconSize Is7x = new("fa-7x");
    public static readonly IconSize Is8x = new("fa-8x");
    public static readonly IconSize Is9x = new("fa-9x");
    public static readonly IconSize Is10x = new("fa-10x");
}
