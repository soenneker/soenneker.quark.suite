using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the type of text utility.
/// </summary>
[Intellenum<string>]
public sealed partial class TextType
{
    public static readonly TextType Start = new("start");
    public static readonly TextType Center = new("center");
    public static readonly TextType End = new("end");
    public static readonly TextType Justify = new("justify");
    public static readonly TextType Wrap = new("wrap");
    public static readonly TextType Nowrap = new("nowrap");
    public static readonly TextType Truncate = new("truncate");
    public static readonly TextType Lowercase = new("lowercase");
    public static readonly TextType Uppercase = new("uppercase");
    public static readonly TextType Capitalize = new("capitalize");
    public static readonly TextType Reset = new("reset");
    public static readonly TextType Muted = new("muted");
    public static readonly TextType Emphasis = new("emphasis");
}
