namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned border width utility.
/// </summary>
public static class Border
{
    public static BorderBuilder Is0 => new(ScaleType.Is0.Value);
    public static BorderBuilder Is1 => new(ScaleType.Is1.Value);
    public static BorderBuilder Is2 => new(ScaleType.Is2.Value);
    public static BorderBuilder Is3 => new(ScaleType.Is3.Value);
    public static BorderBuilder Is4 => new(ScaleType.Is4.Value);
    public static BorderBuilder Is5 => new(ScaleType.Is5.Value);

    /// <summary>
    /// Creates a border width utility from an arbitrary Tailwind token.
    /// </summary>
    public static BorderBuilder Token(string value) => new(value);
}
