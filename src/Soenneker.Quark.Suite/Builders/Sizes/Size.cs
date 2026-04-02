namespace Soenneker.Quark;

/// <summary>
/// Size utility with fluent API.
/// Supports legacy semantic sizes and shadcn/Tailwind size-* tokens.
/// </summary>
public static class Size
{
    public static SizeBuilder Default => new(SizeType.Default);
    public static SizeBuilder ExtraSmall => new(SizeType.ExtraSmall);
    public static SizeBuilder Small => new(SizeType.Small);
    public static SizeBuilder Medium => new(SizeType.Medium);
    public static SizeBuilder Large => new(SizeType.Large);
    public static SizeBuilder ExtraLarge => new(SizeType.ExtraLarge);
    public static SizeBuilder ExtraExtraLarge => new(SizeType.ExtraExtraLarge);

    // shadcn/Tailwind convenience (size-*)
    public static SizeBuilder Is0 => new("0");
    public static SizeBuilder Is1 => new("1");
    public static SizeBuilder Is2 => new("2");
    public static SizeBuilder Is3 => new("3");
    public static SizeBuilder Is4 => new("4");
    public static SizeBuilder Is5 => new("5");
    public static SizeBuilder Is6 => new("6");
    public static SizeBuilder Is7 => new("7");
    public static SizeBuilder Is8 => new("8");
    public static SizeBuilder Is9 => new("9");
    public static SizeBuilder Is10 => new("10");
    public static SizeBuilder Is11 => new("11");
    public static SizeBuilder Is12 => new("12");

    public static SizeBuilder From(SizeType sizeType) => new(sizeType);

    /// <summary>
    /// Create from an arbitrary Tailwind size token (e.g. "5", "full", "[18px]").
    /// </summary>
    public static SizeBuilder Token(string value) => new(value);
}
