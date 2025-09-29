using Intellenum;

namespace Soenneker.Quark.Enums;

[Intellenum<string>]
public sealed partial class IconRotate
{
    public static readonly IconRotate None = new("");
    public static readonly IconRotate R90 = new("fa-rotate-90");
    public static readonly IconRotate R180 = new("fa-rotate-180");
    public static readonly IconRotate R270 = new("fa-rotate-270");
}
