namespace Soenneker.Quark;

internal static class CompositionPresets
{
    internal static readonly QuarkPresetToken Panel = new("composition-panel", c =>
    {
        c.Display = Display.Grid;
        c.Gap = Gap.Is4;
        c.MinWidth = MinWidth.Is0;
        c.Padding = Padding.Is4;
        c.Border = Border.Is1;
        c.BorderColor = BorderColor.Token("[var(--border)]");
        c.Rounded = Rounded.Lg;
    });
    internal static readonly QuarkPresetToken Stack = new("composition-stack", c =>
    {
        c.Display = Display.Grid;
        c.Gap = Gap.Is2;
        c.MinWidth = MinWidth.Is0;
    });
    internal static readonly QuarkPresetToken Header = new("composition-header", c =>
    {
        c.Display = Display.Flex;
        c.FlexWrap = FlexWrap.Wrap;
        c.ItemsAlign = Items.Center;
        c.Justify = Justify.Between;
        c.Gap = Gap.Is3;
        c.MinWidth = MinWidth.Is0;
    });
    internal static readonly QuarkPresetToken Actions = new("composition-actions", c =>
    {
        c.Display = Display.Flex;
        c.FlexWrap = FlexWrap.Wrap;
        c.ItemsAlign = Items.Center;
        c.Gap = Gap.Is2;
    });
    internal static readonly QuarkPresetToken Muted = new("composition-muted", c =>
    {
        c.Margin = Margin.Is0;
        c.TextSize = TextSize.Sm;
        c.TextColor = TextColor.MutedForeground;
    });
    internal static readonly QuarkPresetToken Title = new("composition-title", c =>
    {
        c.Margin = Margin.Is0;
        c.FontWeight = FontWeight.Semibold;
        c.TextSize = TextSize.Lg;
    });
    internal static readonly QuarkPresetToken Code = new("composition-code", c => c.FontFamily = FontFamily.Mono);
}
