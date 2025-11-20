using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents Font Awesome icon family styles.
/// </summary>
[Intellenum<string>]
public sealed partial class IconFamily
{
    /// <summary>
    /// Classic geometry (implicit). Keep empty so we don't emit an extra class.
    /// </summary>
    public static readonly IconFamily Classic = new("");

    /// <summary>
    /// Classic fused duotone (standalone).
    /// </summary>
    public static readonly IconFamily Duotone = new("fa-duotone");

    /// <summary>
    /// Sharp geometry family.
    /// </summary>
    public static readonly IconFamily Sharp = new("fa-sharp");

    /// <summary>
    /// Sharp fused duotone geometry family.
    /// </summary>
    public static readonly IconFamily SharpDuotone = new("fa-sharp-duotone");

    /// <summary>
    /// Chisel geometry family.
    /// </summary>
    public static readonly IconFamily Chisel = new("fa-chisel");

    /// <summary>
    /// Etch geometry family.
    /// </summary>
    public static readonly IconFamily Etch = new("fa-etch");

    /// <summary>
    /// Jelly geometry family.
    /// </summary>
    public static readonly IconFamily Jelly = new("fa-jelly");

    /// <summary>
    /// Jelly duo geometry family.
    /// </summary>
    public static readonly IconFamily JellyDuo = new("fa-jelly-duo");

    /// <summary>
    /// Jelly fill geometry family.
    /// </summary>
    public static readonly IconFamily JellyFill = new("fa-jelly-fill");

    /// <summary>
    /// Notdog geometry family.
    /// </summary>
    public static readonly IconFamily Notdog = new("fa-notdog");

    /// <summary>
    /// Notdog duo geometry family.
    /// </summary>
    public static readonly IconFamily NotdogDuo = new("fa-notdog-duo");

    /// <summary>
    /// Notdog fill geometry family.
    /// </summary>
    public static readonly IconFamily NotdogFill = new("fa-notdog-fill");

    /// <summary>
    /// Slab geometry family.
    /// </summary>
    public static readonly IconFamily Slab = new("fa-slab");

    /// <summary>
    /// Slab press geometry family.
    /// </summary>
    public static readonly IconFamily SlabPress = new("fa-slab-press");

    /// <summary>
    /// Thumbprint geometry family.
    /// </summary>
    public static readonly IconFamily Thumbprint = new("fa-thumbprint");

    /// <summary>
    /// Whiteboard geometry family.
    /// </summary>
    public static readonly IconFamily Whiteboard = new("fa-whiteboard");
}
