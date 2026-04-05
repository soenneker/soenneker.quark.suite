using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class SonnerPosition
{
    public static readonly SonnerPosition TopLeft = new("top-left");
    public static readonly SonnerPosition TopCenter = new("top-center");
    public static readonly SonnerPosition TopRight = new("top-right");
    public static readonly SonnerPosition BottomLeft = new("bottom-left");
    public static readonly SonnerPosition BottomCenter = new("bottom-center");
    public static readonly SonnerPosition BottomRight = new("bottom-right");
}
