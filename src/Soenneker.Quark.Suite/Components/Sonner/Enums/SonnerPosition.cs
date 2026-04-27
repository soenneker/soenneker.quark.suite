using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class SonnerPosition
{
    public static readonly SonnerPosition TopLeft = new(0);
    public static readonly SonnerPosition TopCenter = new(1);
    public static readonly SonnerPosition TopRight = new(2);
    public static readonly SonnerPosition BottomLeft = new(3);
    public static readonly SonnerPosition BottomCenter = new(4);
    public static readonly SonnerPosition BottomRight = new(5);
}
