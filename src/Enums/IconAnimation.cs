using Intellenum;

namespace Soenneker.Quark.Enums;

[Intellenum<string>]
public sealed partial class IconAnimation
{
    public static readonly IconAnimation None = new("");
    public static readonly IconAnimation Spin = new("fa-spin");
    public static readonly IconAnimation SpinRev = new("fa-spin-reverse");
    public static readonly IconAnimation Pulse = new("fa-pulse");
    public static readonly IconAnimation Bounce = new("fa-bounce");
    public static readonly IconAnimation Shake = new("fa-shake");
    public static readonly IconAnimation Beat = new("fa-beat");
    public static readonly IconAnimation Fade = new("fa-fade");
}
