using Soenneker.Quark.Builders;
using Soenneker.Quark.Builders.Animations;
using Soenneker.Quark.Builders.AspectRatios;
using Soenneker.Quark.Builders.BackdropFilters;
using Soenneker.Quark.Builders.BorderRadiuses;
using Soenneker.Quark.Builders.Borders;
using Soenneker.Quark.Builders.BoxShadows;
using Soenneker.Quark.Builders.Clearfix;
using Soenneker.Quark.Builders.ClipPaths;
using Soenneker.Quark.Builders.Colors;
using Soenneker.Quark.Builders.Cursors;
using Soenneker.Quark.Builders.Displays;
using Soenneker.Quark.Builders.Filters;
using Soenneker.Quark.Builders.Flexes;
using Soenneker.Quark.Builders.Floats;
using Soenneker.Quark.Builders.FontStyles;
using Soenneker.Quark.Builders.FontWeights;
using Soenneker.Quark.Builders.Gaps;
using Soenneker.Quark.Builders.Heights;
using Soenneker.Quark.Builders.Interactions;
using Soenneker.Quark.Builders.LineHeights;
using Soenneker.Quark.Builders.Margins;
using Soenneker.Quark.Builders.ObjectFits;
using Soenneker.Quark.Builders.ObjectPositions;
using Soenneker.Quark.Builders.Opacities;
using Soenneker.Quark.Builders.Overflows;
using Soenneker.Quark.Builders.Paddings;
using Soenneker.Quark.Builders.PointerEventss;
using Soenneker.Quark.Builders.PositionOffsets;
using Soenneker.Quark.Builders.Positions;
using Soenneker.Quark.Builders.Resizes;
using Soenneker.Quark.Builders.ScreenReaders;
using Soenneker.Quark.Builders.ScrollBehaviors;
using Soenneker.Quark.Builders.StretchedLinks;
using Soenneker.Quark.Builders.TextAlignments;
using Soenneker.Quark.Builders.TextBreaks;
using Soenneker.Quark.Builders.TextDecorations;
using Soenneker.Quark.Builders.TextOverflows;
using Soenneker.Quark.Builders.TextSizes;
using Soenneker.Quark.Builders.TextTransforms;
using Soenneker.Quark.Builders.TextWraps;
using Soenneker.Quark.Builders.Transforms;
using Soenneker.Quark.Builders.Transitions;
using Soenneker.Quark.Builders.Truncates;
using Soenneker.Quark.Builders.UserSelects;
using Soenneker.Quark.Builders.VerticalAligns;
using Soenneker.Quark.Builders.Visibilities;
using Soenneker.Quark.Builders.Widths;
using Soenneker.Quark.Builders.ZIndexes;

namespace Soenneker.Quark;

public class ComponentOptions
{
    public string ThemeKey { get; set; } = string.Empty;

    public CssValue<DisplayBuilder>? Display { get; set; }

    public CssValue<VisibilityBuilder>? Visibility { get; set; }

    public CssValue<FloatBuilder>? Float { get; set; }

    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    public CssValue<MarginBuilder>? Margin { get; set; }

    public CssValue<PaddingBuilder>? Padding { get; set; }

    public CssValue<PositionBuilder>? Position { get; set; }

    public CssValue<PositionOffsetBuilder>? Offset { get; set; }

    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    public CssValue<WidthBuilder>? Width { get; set; }

    public CssValue<WidthBuilder>? MinWidth { get; set; }

    public CssValue<WidthBuilder>? MaxWidth { get; set; }

    public CssValue<HeightBuilder>? Height { get; set; }

    public CssValue<HeightBuilder>? MinHeight { get; set; }

    public CssValue<HeightBuilder>? MaxHeight { get; set; }

    public CssValue<OverflowBuilder>? Overflow { get; set; }

    public CssValue<OverflowBuilder>? OverflowX { get; set; }

    public CssValue<OverflowBuilder>? OverflowY { get; set; }

    public CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    public CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    public CssValue<TextDecorationBuilder>? TextDecorationLine { get; set; }

    public CssValue<TextDecorationBuilder>? TextDecorationCss { get; set; }

    public CssValue<FlexBuilder>? Flex { get; set; }

    public CssValue<GapBuilder>? Gap { get; set; }

    public CssValue<BorderBuilder>? Border { get; set; }

    public CssValue<OpacityBuilder>? Opacity { get; set; }

    public CssValue<ZIndexBuilder>? ZIndex { get; set; }

    public CssValue<PointerEventsBuilder>? PointerEvents { get; set; }

    public CssValue<UserSelectBuilder>? UserSelect { get; set; }

    public CssValue<TextTransformBuilder>? TextTransform { get; set; }

    public CssValue<FontWeightBuilder>? FontWeight { get; set; }

    public CssValue<FontStyleBuilder>? FontStyle { get; set; }

    public CssValue<LineHeightBuilder>? LineHeight { get; set; }

    public CssValue<TextWrapBuilder>? TextWrap { get; set; }

    public CssValue<TextBreakBuilder>? TextBreak { get; set; }

    public CssValue<ColorBuilder>? TextColor { get; set; }

    public CssValue<ColorBuilder>? BackgroundColor { get; set; }

    public CssValue<ColorBuilder>? TextBackgroundColor { get; set; }

    public CssValue<AnimationBuilder>? Animation { get; set; }

    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    public CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    public CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    public CssValue<ClearfixBuilder>? Clearfix { get; set; }

    public CssValue<ClipPathBuilder>? ClipPath { get; set; }

    public CssValue<CursorBuilder>? Cursor { get; set; }

    public CssValue<FilterBuilder>? Filter { get; set; }

    public CssValue<InteractionBuilder>? Interaction { get; set; }

    public CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    public CssValue<ResizeBuilder>? Resize { get; set; }

    public CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }

    public CssValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    public CssValue<StretchedLinkBuilder>? StretchedLink { get; set; }

    public CssValue<TransformBuilder>? Transform { get; set; }

    public CssValue<TransitionBuilder>? Transition { get; set; }

    public CssValue<TruncateBuilder>? Truncate { get; set; }
}
