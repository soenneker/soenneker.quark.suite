using Intellenum;

namespace Soenneker.Quark.Enums;

[Intellenum<string>]
public sealed partial class IconFamily
{
    // Classic geometry (implicit). Keep empty so we don't emit an extra class.
    public static readonly IconFamily Classic = new("");

    // Classic fused duotone (standalone)
    public static readonly IconFamily Duotone = new("fa-duotone");

    // Geometry families
    public static readonly IconFamily Sharp = new("fa-sharp");
    public static readonly IconFamily SharpDuotone = new("fa-sharp-duotone"); // fused

    public static readonly IconFamily Chisel = new("fa-chisel");

    public static readonly IconFamily Etch = new("fa-etch");

    public static readonly IconFamily Jelly = new("fa-jelly");
    public static readonly IconFamily JellyDuo = new("fa-jelly-duo");
    public static readonly IconFamily JellyFill = new("fa-jelly-fill");

    public static readonly IconFamily Notdog = new("fa-notdog");
    public static readonly IconFamily NotdogDuo = new("fa-notdog-duo");
    public static readonly IconFamily NotdogFill = new("fa-notdog-fill");

    public static readonly IconFamily Slab = new("fa-slab");
    public static readonly IconFamily SlabPress = new("fa-slab-press");

    public static readonly IconFamily Thumbprint = new("fa-thumbprint");
    public static readonly IconFamily Whiteboard = new("fa-whiteboard");
}
