namespace Soenneker.Quark;

/// <summary>
/// Width utility with fluent API and Tailwind-first mappings.
/// </summary>
public static class Width
{
    /// <summary>
    /// <c>w-25</c> — width from the same spacing scale as padding/margin (<c>25</c> → <c>6.25rem</c> in Tailwind’s default theme).
    /// </summary>
    public static WidthBuilder Is25 => new("25");
    /// <summary>
    /// <c>w-50</c> — spacing scale step <c>50</c> (<c>12.5rem</c> by default).
    /// </summary>
    public static WidthBuilder Is50 => new("50");
    /// <summary>
    /// <c>w-75</c> — spacing scale step <c>75</c> (<c>18.75rem</c> by default).
    /// </summary>
    public static WidthBuilder Is75 => new("75");
    /// <summary>
    /// <c>w-100</c> — spacing scale step <c>100</c> (<c>25rem</c> by default).
    /// </summary>
    public static WidthBuilder Is100 => new("100");

    /// <summary>
    /// <c>w-0</c> — <c>width: 0px</c>.
    /// </summary>
    public static WidthBuilder Is0 => new("0");
    /// <summary>
    /// <c>w-px</c> — fixed <c>1px</c> width (hairline columns/dividers).
    /// </summary>
    public static WidthBuilder IsPx => new("px");
    /// <summary>
    /// <c>w-full</c> — <c>width: 100%</c> of the parent (block-level fill).
    /// </summary>
    public static WidthBuilder IsFull => new("full");
    /// <summary>
    /// <c>w-screen</c> — <c>100vw</c> (viewport width; can overflow if scrollbars differ from <c>100%</c>).
    /// </summary>
    public static WidthBuilder IsScreen => new("screen");
    /// <summary>
    /// <c>w-fit</c> — <c>width: fit-content</c> (shrink-wrap to content).
    /// </summary>
    public static WidthBuilder IsFit => new("fit");
    /// <summary>
    /// <c>w-min</c> — <c>width: min-content</c> (as narrow as content allows).
    /// </summary>
    public static WidthBuilder IsMin => new("min");
    /// <summary>
    /// <c>w-max</c> — <c>width: max-content</c> (as wide as content needs).
    /// </summary>
    public static WidthBuilder IsMax => new("max");

    /// <summary>
    /// <c>w-1/2</c> — <c>width: 50%</c> of the container (flex/grid column layouts).
    /// </summary>
    public static WidthBuilder Is1of2 => new("1/2");
    /// <summary>
    /// <c>w-1/3</c> — <c>width: 33.333333%</c>.
    /// </summary>
    public static WidthBuilder Is1of3 => new("1/3");
    /// <summary>
    /// <c>w-2/3</c> — <c>width: 66.666667%</c>.
    /// </summary>
    public static WidthBuilder Is2of3 => new("2/3");
    /// <summary>
    /// <c>w-1/4</c> — <c>width: 25%</c>.
    /// </summary>
    public static WidthBuilder Is1of4 => new("1/4");
    /// <summary>
    /// <c>w-3/4</c> — <c>width: 75%</c>.
    /// </summary>
    public static WidthBuilder Is3of4 => new("3/4");
    /// <summary>
    /// <c>w-1/5</c> — <c>width: 20%</c>.
    /// </summary>
    public static WidthBuilder Is1of5 => new("1/5");
    /// <summary>
    /// <c>w-2/5</c> — <c>width: 40%</c>.
    /// </summary>
    public static WidthBuilder Is2of5 => new("2/5");
    /// <summary>
    /// <c>w-3/5</c> — <c>width: 60%</c>.
    /// </summary>
    public static WidthBuilder Is3of5 => new("3/5");
    /// <summary>
    /// <c>w-4/5</c> — <c>width: 80%</c>.
    /// </summary>
    public static WidthBuilder Is4of5 => new("4/5");
    /// <summary>
    /// <c>w-1/6</c> — <c>width: 16.666667%</c>.
    /// </summary>
    public static WidthBuilder Is1of6 => new("1/6");
    /// <summary>
    /// <c>w-5/6</c> — <c>width: 83.333333%</c>.
    /// </summary>
    public static WidthBuilder Is5of6 => new("5/6");

    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public static WidthBuilder Auto => new("auto");

    /// <summary>
    /// Create from an arbitrary Tailwind width token (e.g. "72", "[18rem]", "full").
    /// </summary>
    public static WidthBuilder Token(string token) => new(token);
}
