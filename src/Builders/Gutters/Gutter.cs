using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Static helper for Bootstrap gutter utilities.
/// </summary>
public static class Gutter
{
    public static GutterBuilder X(int value) => new GutterBuilder().X(value);
    public static GutterBuilder Y(int value) => new GutterBuilder().Y(value);
    public static GutterBuilder All(int value) => new GutterBuilder().All(value);
    public static GutterBuilder None => new GutterBuilder().None();
}
