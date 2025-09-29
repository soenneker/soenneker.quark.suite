namespace Soenneker.Quark.Builders.Flexes;

/// <summary>
/// Simplified flex utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Flex
{
    /// <summary>
    /// Display flex.
    /// </summary>
    public static FlexBuilder Display => new("display");

    /// <summary>
    /// Flex direction row.
    /// </summary>
    public static FlexBuilder Row => new("direction", "row");

    /// <summary>
    /// Flex direction column.
    /// </summary>
    public static FlexBuilder Column => new("direction", "column");

    /// <summary>
    /// Flex wrap.
    /// </summary>
    public static FlexBuilder Wrap => new("wrap", "wrap");

    /// <summary>
    /// Flex nowrap.
    /// </summary>
    public static FlexBuilder NoWrap => new("wrap", "nowrap");

    /// <summary>
    /// Justify content start.
    /// </summary>
    public static FlexBuilder JustifyStart => new("justify", "start");

    /// <summary>
    /// Justify content end.
    /// </summary>
    public static FlexBuilder JustifyEnd => new("justify", "end");

    /// <summary>
    /// Justify content center.
    /// </summary>
    public static FlexBuilder JustifyCenter => new("justify", "center");

    /// <summary>
    /// Justify content between.
    /// </summary>
    public static FlexBuilder JustifyBetween => new("justify", "between");

    /// <summary>
    /// Justify content around.
    /// </summary>
    public static FlexBuilder JustifyAround => new("justify", "around");

    /// <summary>
    /// Justify content evenly.
    /// </summary>
    public static FlexBuilder JustifyEvenly => new("justify", "evenly");

    /// <summary>
    /// Align items start.
    /// </summary>
    public static FlexBuilder AlignStart => new("align", "start");

    /// <summary>
    /// Align items end.
    /// </summary>
    public static FlexBuilder AlignEnd => new("align", "end");

    /// <summary>
    /// Align items center.
    /// </summary>
    public static FlexBuilder AlignCenter => new("align", "center");

    /// <summary>
    /// Align items baseline.
    /// </summary>
    public static FlexBuilder AlignBaseline => new("align", "baseline");

    /// <summary>
    /// Align items stretch.
    /// </summary>
    public static FlexBuilder AlignStretch => new("align", "stretch");
}
