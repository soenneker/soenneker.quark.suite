namespace Soenneker.Quark;

internal static class EventTimelinePresets
{
    internal static readonly QuarkPresetToken VerticalContentPreset = new("event-timeline-verticalcontent", static context => context.Class = "min-w-0 text-sm text-muted-foreground break-words");

    internal static readonly QuarkPresetToken VerticalTimePreset = new("event-timeline-verticaltime", static context => context.Class = "text-sm font-medium break-words");

    internal static readonly QuarkPresetToken VerticalMarkerPreset = new("event-timeline-verticalmarker", static context => context.Class = "pointer-events-none absolute left-0 top-2 size-2 rounded-full bg-current");

    internal static readonly QuarkPresetToken VerticalConnectorPreset = new("event-timeline-verticalconnector", static context => context.Class = "pointer-events-none absolute left-1 top-3 bottom-0 w-px bg-border");

    internal static readonly QuarkPresetToken VerticalItemPreset = new("event-timeline-verticalitem", static context => context.Class = "relative min-w-0 pl-6 pb-5 last:pb-0");

    internal static readonly QuarkPresetToken VerticalTrackPreset = new("event-timeline-verticaltrack", static context => context.Class = "flex min-w-0 flex-col");

    internal static readonly QuarkPresetToken VerticalSurfacePreset = new("event-timeline-verticalsurface", static context => context.Class = "min-w-0");

    internal static readonly QuarkPresetToken RootPreset = new("event-timeline-root", static context =>
    {
        context.Padding = Padding.Is0;
        context.MinWidth = MinWidth.Is0;
    });

    internal static readonly QuarkPresetToken SurfacePreset = new("event-timeline-surface", static context =>
    {
        context.Position = Position.Relative;
        context.OverflowX = Overflow.X.Auto;
        context.OverflowY = Overflow.Y.Hidden;
        context.BackgroundColor = BackgroundColor.Transparent;
        context.Padding = Padding.Is8;
    });

    internal static readonly QuarkPresetToken ViewControlsPreset = new("event-timeline-view-controls", static context =>
    {
        context.Display = Display.Flex;
        context.ItemsAlign = global::Soenneker.Quark.Items.Center;
        context.Justify = Justify.Between;
        context.Gap = Gap.Is3;
        context.Padding = Padding.OnX.Is3.OnY.Is2;
        context.Border = Border.Is1;
        context.BorderColor = BorderColor.Token("[var(--border)]");
        context.BackgroundColor = BackgroundColor.Token("[var(--background)]");
    });

    internal static readonly QuarkPresetToken ViewControlsLabelPreset = new("event-timeline-view-controls-label", static context =>
    {
        context.TextSize = TextSize.Xs;
        context.FontWeight = FontWeight.Medium;
        context.TextColor = TextColor.Token("[var(--muted-foreground)]");
    });

    internal static readonly QuarkPresetToken ViewControlsButtonsPreset = new("event-timeline-view-controls-buttons", static context =>
    {
        context.Display = Display.Flex;
        context.ItemsAlign = global::Soenneker.Quark.Items.Center;
        context.Gap = Gap.Is1;
    });

    internal static readonly QuarkPresetToken EmptyPreset = new("event-timeline-empty", static context =>
    {
        context.TextSize = TextSize.Sm;
        context.TextColor = TextColor.Token("[var(--muted-foreground)]");
    });

    internal static readonly QuarkPresetToken TrackPreset = new("event-timeline-track", static context =>
    {
        context.Position = Position.Relative;
        context.Display = Display.Flex;
        context.Width = Width.IsFull;
        context.MinWidth = MinWidth.Is0;
        context.Justify = Justify.Start;
        context.Gap = Gap.Is0;
    });

    internal static readonly QuarkPresetToken ScrollItemPreset = new("event-timeline-item-scroll", static context =>
    {
        ApplyItemBase(context);
        context.Width = Width.Token("[12rem]");
        context.Grow = Grow.Is0;
        context.Shrink = Shrink.Is0;
        context.Class = "basis-[12rem]";
    });

    internal static readonly QuarkPresetToken FitItemPreset = new("event-timeline-item-fit", static context =>
    {
        ApplyItemBase(context);
        context.Width = Width.Token("[auto]");
        context.Grow = Grow.Is1;
        context.Shrink = Shrink.Is1;
        context.Class = "basis-0";
    });

    internal static readonly QuarkPresetToken ConnectorPreset = new("event-timeline-connector", static context =>
    {
        context.Position = Position.Absolute;
        context.Left = Left.Token("[calc(50%+1.25rem)]");
        context.Top = Top.Token("[1.25rem]");
        context.Height = Height.Token("[2px]");
        context.Width = Width.Token("[calc(100%-2.5rem)]");
        context.BackgroundColor = BackgroundColor.Token("[color-mix(in_oklch,var(--foreground),var(--border)_38%)]");
        context.PointerEvents = PointerEvents.None;
    });

    internal static readonly QuarkPresetToken TimePreset = new("event-timeline-time", static context =>
    {
        context.Whitespace = Whitespace.Nowrap;
        context.MaxWidth = MaxWidth.IsFull;
        context.Overflow = Overflow.Hidden;
        context.TextOverflow = TextOverflow.Ellipsis;
        context.TextAlign = TextAlign.Center;
        context.TextSize = TextSize.Xs;
        context.Leading = Leading.Is4;
        context.TextColor = TextColor.Token("[var(--muted-foreground)]");
    });

    internal static readonly QuarkPresetToken MarkerNeutralPreset = new("event-timeline-marker-neutral", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = TextColor.Token("[var(--foreground)]");
    });

    internal static readonly QuarkPresetToken MarkerSuccessPreset = new("event-timeline-marker-success", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = TextColor.Token("emerald-700").OnDark.Token("emerald-400");
    });

    internal static readonly QuarkPresetToken MarkerInfoPreset = new("event-timeline-marker-info", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = TextColor.Token("sky-700").OnDark.Token("sky-400");
    });

    internal static readonly QuarkPresetToken MarkerWarningPreset = new("event-timeline-marker-warning", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = TextColor.Token("amber-700").OnDark.Token("amber-400");
    });

    internal static readonly QuarkPresetToken MarkerDangerPreset = new("event-timeline-marker-danger", static context =>
    {
        ApplyMarkerBase(context);
        context.TextColor = TextColor.Token("[var(--destructive)]");
    });

    internal static readonly QuarkPresetToken LabelNeutralPreset = new("event-timeline-label-neutral", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = BackgroundColor.Token("[var(--muted)]");
        context.TextColor = TextColor.Token("[var(--foreground)]");
    });

    internal static readonly QuarkPresetToken LabelSuccessPreset = new("event-timeline-label-success", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = BackgroundColor.Token("emerald-500/10");
        context.TextColor = TextColor.Token("emerald-700").OnDark.Token("emerald-400");
    });

    internal static readonly QuarkPresetToken LabelInfoPreset = new("event-timeline-label-info", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = BackgroundColor.Token("sky-500/10");
        context.TextColor = TextColor.Token("sky-700").OnDark.Token("sky-400");
    });

    internal static readonly QuarkPresetToken LabelWarningPreset = new("event-timeline-label-warning", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = BackgroundColor.Token("amber-500/10");
        context.TextColor = TextColor.Token("amber-700").OnDark.Token("amber-400");
    });

    internal static readonly QuarkPresetToken LabelDangerPreset = new("event-timeline-label-danger", static context =>
    {
        ApplyLabelBase(context);
        context.BackgroundColor = BackgroundColor.Token("[var(--muted)]");
        context.TextColor = TextColor.Token("[var(--destructive)]");
    });



    private static void ApplyMarkerBase(QuarkPresetContext context)
    {
        context.Position = Position.Relative;
        context.Display = Display.InlineFlex;
        context.Size = Size.Is10;
        context.ItemsAlign = global::Soenneker.Quark.Items.Center;
        context.Justify = Justify.Center;
        context.Rounded = Rounded.Xl;
        context.Border = Border.Is1;
        context.BorderColor = BorderColor.Token("[var(--border)]");
        context.BackgroundColor = BackgroundColor.Token("[var(--background)]");
        context.ZIndex = ZIndex.Is10;
        context.Class = "shadow-[0_1px_2px_color-mix(in_oklch,var(--foreground),transparent_88%),inset_0_1px_0_color-mix(in_oklch,var(--foreground),transparent_94%)] [&_svg]:size-5";
    }

    private static void ApplyItemBase(QuarkPresetContext context)
    {
        context.Position = Position.Relative;
        context.Display = Display.Flex;
        context.MinWidth = MinWidth.Is0;
        context.FlexDirection = FlexDirection.Col;
        context.ItemsAlign = global::Soenneker.Quark.Items.Center;
        context.Justify = Justify.Center;
        context.Gap = Gap.Is2;
    }

    private static void ApplyLabelBase(QuarkPresetContext context)
    {
        context.Display = Display.InlineFlex;
        context.Height = Height.Is6;
        context.ItemsAlign = global::Soenneker.Quark.Items.Center;
        context.Rounded = Rounded.Lg;
        context.Padding = Padding.OnX.Is2;
        context.MaxWidth = MaxWidth.IsFull;
        context.Overflow = Overflow.Hidden;
        context.TextOverflow = TextOverflow.Ellipsis;
        context.Whitespace = Whitespace.Nowrap;
        context.TextSize = TextSize.Xs;
        context.FontWeight = FontWeight.Medium;
        context.TextTransform = TextTransform.Capitalize;
        context.Leading = Leading.None;
    }

}
