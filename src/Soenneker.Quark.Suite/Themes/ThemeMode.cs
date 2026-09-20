using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>The preferred application color scheme.</summary>
[EnumValue<string>]
public sealed partial class ThemeMode
{
    /// <summary>Follow the operating system color scheme.</summary>
    public static readonly ThemeMode System = new("system");
    /// <summary>Always use the light color scheme.</summary>
    public static readonly ThemeMode Light = new("light");
    /// <summary>Always use the dark color scheme.</summary>
    public static readonly ThemeMode Dark = new("dark");
}
