using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class SonnerToastType
{
    public static readonly SonnerToastType Default = new(0);
    public static readonly SonnerToastType Success = new(1);
    public static readonly SonnerToastType Info = new(2);
    public static readonly SonnerToastType Warning = new(3);
    public static readonly SonnerToastType Error = new(4);
    public static readonly SonnerToastType Loading = new(5);
}
