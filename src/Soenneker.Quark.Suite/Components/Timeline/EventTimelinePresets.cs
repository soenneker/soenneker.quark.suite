namespace Soenneker.Quark;

internal static class EventTimelinePresets
{
    private static readonly CssValue<PaddingBuilder> _css0 = Padding.Is0;
    private static readonly CssValue<MinWidthBuilder> _css1 = MinWidth.Is0;
    private static readonly CssValue<PositionBuilder> _css2 = Position.Relative;
    private static readonly CssValue<OverflowBuilder> _css3 = Overflow.X.Auto;
    private static readonly CssValue<OverflowBuilder> _css4 = Overflow.Y.Hidden;
    private static readonly CssValue<BackgroundColorBuilder> _css5 = BackgroundColor.Transparent;
    private static readonly CssValue<PaddingBuilder> _css6 = Padding.Is8;
    private static readonly CssValue<DisplayBuilder> _css7 = Display.Flex;
    private static readonly CssValue<ItemsBuilder> _css8 = global::Soenneker.Quark.Items.Center;
    private static readonly CssValue<JustifyBuilder> _css9 = Justify.Between;
    private static readonly CssValue<GapBuilder> _css10 = Gap.Is3;
    private static readonly CssValue<PaddingBuilder> _css11 = Padding.OnX.Is3.OnY.Is2;
    private static readonly CssValue<BorderBuilder> _css12 = Border.Is1;
    private static readonly CssValue<BorderColorBuilder> _css13 = BorderColor.Token("[var(--border)]");
    private static readonly CssValue<BackgroundColorBuilder> _css14 = BackgroundColor.Token("[var(--background)]");
    private static readonly CssValue<TextSizeBuilder> _css15 = TextSize.Xs;
    private static readonly CssValue<FontWeightBuilder> _css16 = FontWeight.Medium;
    private static readonly CssValue<TextColorBuilder> _css17 = TextColor.Token("[var(--muted-foreground)]");
    private static readonly CssValue<GapBuilder> _css18 = Gap.Is1;
    private static readonly CssValue<TextSizeBuilder> _css19 = TextSize.Sm;
    private static readonly CssValue<WidthBuilder> _css20 = Width.IsFull;
    private static readonly CssValue<JustifyBuilder> _css21 = Justify.Start;
    private static readonly CssValue<GapBuilder> _css22 = Gap.Is0;
    private static readonly CssValue<WidthBuilder> _css23 = Width.Token("[12rem]");
    private static readonly CssValue<GrowBuilder> _css24 = Grow.Is0;
    private static readonly CssValue<ShrinkBuilder> _css25 = Shrink.Is0;
    private static readonly CssValue<WidthBuilder> _css26 = Width.Token("[auto]");
    private static readonly CssValue<GrowBuilder> _css27 = Grow.Is1;
    private static readonly CssValue<ShrinkBuilder> _css28 = Shrink.Is1;
    private static readonly CssValue<PositionBuilder> _css29 = Position.Absolute;
    private static readonly CssValue<LeftBuilder> _css30 = Left.Token("[calc(50%+1.25rem)]");
    private static readonly CssValue<TopBuilder> _css31 = Top.Token("[1.25rem]");
    private static readonly CssValue<HeightBuilder> _css32 = Height.Token("[2px]");
    private static readonly CssValue<WidthBuilder> _css33 = Width.Token("[calc(100%-2.5rem)]");
    private static readonly CssValue<BackgroundColorBuilder> _css34 = BackgroundColor.Token("[color-mix(in_oklch,var(--foreground),var(--border)_38%)]");
    private static readonly CssValue<PointerEventsBuilder> _css35 = PointerEvents.None;
    private static readonly CssValue<WhitespaceBuilder> _css36 = Whitespace.Nowrap;
    private static readonly CssValue<MaxWidthBuilder> _css37 = MaxWidth.IsFull;
    private static readonly CssValue<OverflowBuilder> _css38 = Overflow.Hidden;
    private static readonly CssValue<TextOverflowBuilder> _css39 = TextOverflow.Ellipsis;
    private static readonly CssValue<TextAlignBuilder> _css40 = TextAlign.Center;
    private static readonly CssValue<LeadingBuilder> _css41 = Leading.Is4;
    private static readonly CssValue<TextColorBuilder> _css42 = TextColor.Token("[var(--foreground)]");
    private static readonly CssValue<TextColorBuilder> _css43 = TextColor.Token("emerald-700").OnDark.Token("emerald-400");
    private static readonly CssValue<TextColorBuilder> _css44 = TextColor.Token("sky-700").OnDark.Token("sky-400");
    private static readonly CssValue<TextColorBuilder> _css45 = TextColor.Token("amber-700").OnDark.Token("amber-400");
    private static readonly CssValue<TextColorBuilder> _css46 = TextColor.Token("[var(--destructive)]");
    private static readonly CssValue<BackgroundColorBuilder> _css47 = BackgroundColor.Token("[var(--muted)]");
    private static readonly CssValue<BackgroundColorBuilder> _css48 = BackgroundColor.Token("emerald-500/10");
    private static readonly CssValue<BackgroundColorBuilder> _css49 = BackgroundColor.Token("sky-500/10");
    private static readonly CssValue<BackgroundColorBuilder> _css50 = BackgroundColor.Token("amber-500/10");
    private static readonly CssValue<DisplayBuilder> _css51 = Display.InlineFlex;
    private static readonly CssValue<SizeBuilder> _css52 = Size.Is10;
    private static readonly CssValue<JustifyBuilder> _css53 = Justify.Center;
    private static readonly CssValue<RoundedBuilder> _css54 = Rounded.Xl;
    private static readonly CssValue<ZIndexBuilder> _css55 = ZIndex.Is10;
    private static readonly CssValue<FlexDirectionBuilder> _css56 = FlexDirection.Col;
    private static readonly CssValue<GapBuilder> _css57 = Gap.Is2;
    private static readonly CssValue<HeightBuilder> _css58 = Height.Is6;
    private static readonly CssValue<RoundedBuilder> _css59 = Rounded.Lg;
    private static readonly CssValue<PaddingBuilder> _css60 = Padding.OnX.Is2;
    private static readonly CssValue<TextTransformBuilder> _css61 = TextTransform.Capitalize;
    private static readonly CssValue<LeadingBuilder> _css62 = Leading.None;

    internal static readonly QuarkPresetToken VerticalContentPreset = new("event-timeline-verticalcontent", static context => context.Class = "min-w-0 text-sm text-muted-foreground break-words");

    internal static readonly QuarkPresetToken VerticalTimePreset = new("event-timeline-verticaltime", static context => context.Class = "text-sm font-medium break-words");

    internal static readonly QuarkPresetToken VerticalMarkerPreset = new("event-timeline-verticalmarker", static context => context.Class = "pointer-events-none absolute left-0 top-2 size-2 rounded-full bg-current");

    internal static readonly QuarkPresetToken VerticalConnectorPreset = new("event-timeline-verticalconnector", static context => context.Class = "pointer-events-none absolute left-1 top-3 bottom-0 w-px bg-border");

    internal static readonly QuarkPresetToken VerticalItemPreset = new("event-timeline-verticalitem", static context => context.Class = "relative min-w-0 pl-6 pb-5 last:pb-0");

    internal static readonly QuarkPresetToken VerticalTrackPreset = new("event-timeline-verticaltrack", static context => context.Class = "flex min-w-0 flex-col");

    internal static readonly QuarkPresetToken VerticalSurfacePreset = new("event-timeline-verticalsurface", static context => context.Class = "min-w-0");

    internal static readonly QuarkPresetToken RootPreset = new("event-timeline-root", static context =>
    {
        context.Padding = _css0;
        context.MinWidth = _css1;
    });

    internal static readonly QuarkPresetToken SurfacePreset = new("event-timeline-surface", static context =>
    {
        context.Position = _css2;
        context.OverflowX = _css3;
        context.OverflowY = _css4;
        context.BackgroundColor = _css5;
        context.Padding = _css6;
    });

    internal static readonly QuarkPresetToken ViewControlsPreset = new("event-timeline-view-controls", static context =>
    {
        context.Display = _css7;
        context.ItemsAlign = _css8;
        context.Justify = _css9;
        context.Gap = _css10;
        context.Padding = _css11;
        context.Border = _css12;
        context.BorderColor = _css13;
        context.BackgroundColor = _css14;
    });

    internal static readonly QuarkPresetToken ViewControlsLabelPreset = new("event-timeline-view-controls-label", static context =>
    {
        context.TextSize = _css15;
        context.FontWeight = _css16;
        context.TextColor = _css17;
    });

    internal static readonly QuarkPresetToken ViewControlsButtonsPreset = new("event-timeline-view-controls-buttons", static context =>
    {
        context.Display = _css7;
        context.ItemsAlign = _css8;
        context.Gap = _css18;
    });

    internal static readonly QuarkPresetToken EmptyPreset = new("event-timeline-empty", static context =>
    {
        context.TextSize = _css19;
        context.TextColor = _css17;
    });

    internal static readonly QuarkPresetToken TrackPreset = new("event-timeline-track", static context =>
    {
        context.Position = _css2;
        context.Display = _css7;
        context.Width = _css20;
        context.MinWidth = _css1;
        context.Justify = _css21;
        context.Gap = _css22;
    });

    internal static readonly QuarkPresetToken ScrollItemPreset = new("event-timeline-item-scroll", static context =>
    {
        ApplyItemBase(context);
        context.Width = _css23;
        context.Grow = _css24;
        context.Shrink = _css25;
        context.Class = "basis-[12rem]";
    });

    internal static readonly QuarkPresetToken FitItemPreset = new("event-timeline-item-fit", static context =>
    {
        ApplyItemBase(context);
        context.Width = _css26;
        context.Grow = _css27;
        context.Shrink = _css28;
        context.Class = "basis-0";
    });

    internal static readonly QuarkPresetToken ConnectorPreset = new("event-timeline-connector", static context =>
    {
        context.Position = _css29;
        context.Left = _css30;
        context.Top = _css31;
        context.Height = _css32;
        context.Width = _css33;
        context.BackgroundColor = _css34;
        context.PointerEvents = _css35;
    });

    internal static readonly QuarkPresetToken TimePreset = new("event-timeline-time", static context =>
    {
        context.Whitespace = _css36;
        context.MaxWidth = _css37;
        context.Overflow = _css38;
        context.TextOverflow = _css39;
        context.TextAlign = _css40;
        context.TextSize = _css15;
        context.Leading = _css41;
        context.TextColor = _css17;
    });

    internal static readonly QuarkPresetToken MarkerNeutralPreset = new("event-timeline-marker-neutral", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = _css42;
    });

    internal static readonly QuarkPresetToken MarkerSuccessPreset = new("event-timeline-marker-success", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = _css43;
    });

    internal static readonly QuarkPresetToken MarkerInfoPreset = new("event-timeline-marker-info", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = _css44;
    });

    internal static readonly QuarkPresetToken MarkerWarningPreset = new("event-timeline-marker-warning", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = _css45;
    });

    internal static readonly QuarkPresetToken MarkerDangerPreset = new("event-timeline-marker-danger", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = _css46;
    });

    internal static readonly QuarkPresetToken LabelNeutralPreset = new("event-timeline-label-neutral", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = _css47;
        context.TextColor = _css42;
    });

    internal static readonly QuarkPresetToken LabelSuccessPreset = new("event-timeline-label-success", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = _css48;
        context.TextColor = _css43;
    });

    internal static readonly QuarkPresetToken LabelInfoPreset = new("event-timeline-label-info", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = _css49;
        context.TextColor = _css44;
    });

    internal static readonly QuarkPresetToken LabelWarningPreset = new("event-timeline-label-warning", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = _css50;
        context.TextColor = _css45;
    });

    internal static readonly QuarkPresetToken LabelDangerPreset = new("event-timeline-label-danger", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = _css47;
        context.TextColor = _css46;
    });



    private static void ApplyMarkerBase(QuarkPresetContext context)
    {
        context.Position = _css2;
        context.Display = _css51;
        context.Size = _css52;
        context.ItemsAlign = _css8;
        context.Justify = _css53;
        context.Rounded = _css54;
        context.Border = _css12;
        context.BorderColor = _css13;
        context.BackgroundColor = _css14;
        context.ZIndex = _css55;
        context.Class = "shadow-[0_1px_2px_color-mix(in_oklch,var(--foreground),transparent_88%),inset_0_1px_0_color-mix(in_oklch,var(--foreground),transparent_94%)] [&_svg]:size-5";
    }

    private static void ApplyItemBase(QuarkPresetContext context)
    {
        context.Position = _css2;
        context.Display = _css7;
        context.MinWidth = _css1;
        context.FlexDirection = _css56;
        context.ItemsAlign = _css8;
        context.Justify = _css53;
        context.Gap = _css57;
    }

    private static void ApplyLabelBase(QuarkPresetContext context)
    {
        context.Display = _css51;
        context.Height = _css58;
        context.ItemsAlign = _css8;
        context.Rounded = _css59;
        context.Padding = _css60;
        context.MaxWidth = _css37;
        context.Overflow = _css38;
        context.TextOverflow = _css39;
        context.Whitespace = _css36;
        context.TextSize = _css15;
        context.FontWeight = _css16;
        context.TextTransform = _css61;
        context.Leading = _css62;
    }

}
