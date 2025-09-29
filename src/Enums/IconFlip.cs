using Intellenum;

namespace Soenneker.Quark;

[Intellenum<string>]
public sealed partial class IconFlip
{
    public static readonly IconFlip None = new("");
    public static readonly IconFlip Horizontal = new("fa-flip-horizontal");
    public static readonly IconFlip Vertical = new("fa-flip-vertical");
    public static readonly IconFlip Both = new("fa-flip-both");
}
