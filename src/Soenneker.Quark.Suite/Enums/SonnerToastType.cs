using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class SonnerToastType
{
    public static readonly SonnerToastType Default = new("default");
    public static readonly SonnerToastType Success = new("success");
    public static readonly SonnerToastType Info = new("info");
    public static readonly SonnerToastType Warning = new("warning");
    public static readonly SonnerToastType Error = new("error");
    public static readonly SonnerToastType Loading = new("loading");
}
