using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned padding utility.
/// </summary>
public static class Padding
{
    public static PaddingBuilder Is0 => new(ScaleType.Is0Value);
    public static PaddingBuilder Is1 => new(ScaleType.Is1Value);
    public static PaddingBuilder Is2 => new(ScaleType.Is2Value);
    public static PaddingBuilder Is3 => new(ScaleType.Is3Value);
    public static PaddingBuilder Is4 => new(ScaleType.Is4Value);
    public static PaddingBuilder Is5 => new(ScaleType.Is5Value);
    public static PaddingBuilder Is6 => new("6");
    public static PaddingBuilder Is8 => new("8");
    public static PaddingBuilder Is16 => new("16");

    /// <summary>
    /// Creates a padding utility from an arbitrary Tailwind spacing token.
    /// </summary>
    public static PaddingBuilder Token(string value) => new(value);
}
