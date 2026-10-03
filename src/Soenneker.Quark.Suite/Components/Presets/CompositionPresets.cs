namespace Soenneker.Quark;

internal static class CompositionPresets
{
    private static readonly CssValue<DisplayBuilder> _css0 = Display.Grid;
    private static readonly CssValue<GapBuilder> _css1 = Gap.Is4;
    private static readonly CssValue<MinWidthBuilder> _css2 = MinWidth.Is0;
    private static readonly CssValue<PaddingBuilder> _css3 = Padding.Is4;
    private static readonly CssValue<BorderBuilder> _css4 = Border.Is1;
    private static readonly CssValue<BorderColorBuilder> _css5 = BorderColor.Token("[var(--border)]");
    private static readonly CssValue<RoundedBuilder> _css6 = Rounded.Lg;
    private static readonly CssValue<GapBuilder> _css7 = Gap.Is2;
    private static readonly CssValue<DisplayBuilder> _css8 = Display.Flex;
    private static readonly CssValue<FlexWrapBuilder> _css9 = FlexWrap.Wrap;
    private static readonly CssValue<ItemsBuilder> _css10 = Items.Center;
    private static readonly CssValue<JustifyBuilder> _css11 = Justify.Between;
    private static readonly CssValue<GapBuilder> _css12 = Gap.Is3;
    private static readonly CssValue<MarginBuilder> _css13 = Margin.Is0;
    private static readonly CssValue<TextSizeBuilder> _css14 = TextSize.Sm;
    private static readonly CssValue<TextColorBuilder> _css15 = TextColor.MutedForeground;
    private static readonly CssValue<FontWeightBuilder> _css16 = FontWeight.Semibold;
    private static readonly CssValue<TextSizeBuilder> _css17 = TextSize.Lg;
    private static readonly CssValue<FontFamilyBuilder> _css18 = FontFamily.Mono;

    internal static readonly QuarkPresetToken Panel = new("composition-panel", c =>
    {
        c.Display = _css0;
        c.Gap = _css1;
        c.MinWidth = _css2;
        c.Padding = _css3;
        c.Border = _css4;
        c.BorderColor = _css5;
        c.Rounded = _css6;
    });
    internal static readonly QuarkPresetToken Stack = new("composition-stack", c =>
    {
        c.Display = _css0;
        c.Gap = _css7;
        c.MinWidth = _css2;
    });
    internal static readonly QuarkPresetToken Header = new("composition-header", c =>
    {
        c.Display = _css8;
        c.FlexWrap = _css9;
        c.ItemsAlign = _css10;
        c.Justify = _css11;
        c.Gap = _css12;
        c.MinWidth = _css2;
    });
    internal static readonly QuarkPresetToken Actions = new("composition-actions", c =>
    {
        c.Display = _css8;
        c.FlexWrap = _css9;
        c.ItemsAlign = _css10;
        c.Gap = _css7;
    });
    internal static readonly QuarkPresetToken Muted = new("composition-muted", c =>
    {
        c.Margin = _css13;
        c.TextSize = _css14;
        c.TextColor = _css15;
    });
    internal static readonly QuarkPresetToken Title = new("composition-title", c =>
    {
        c.Margin = _css13;
        c.FontWeight = _css16;
        c.TextSize = _css17;
    });
    internal static readonly QuarkPresetToken Code = new("composition-code", c => c.FontFamily = _css18);
}
