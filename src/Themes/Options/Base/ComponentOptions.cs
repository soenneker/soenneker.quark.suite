using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class ComponentOptions
{
    /// <summary>Gets or sets the CSS selector for this component (e.g., "a", "i", ":root").</summary>
    public string Selector { get; set; } = ":root";

    internal virtual IEnumerable<ComponentCssRule> GetCssRules()
    {
        if (Selector.IsNullOrWhiteSpace())
            return [];

        var buffer = new List<ComponentCssRule>(32);
        CollectCssRules(buffer, Selector);
        return buffer;
    }

    public CssValue<DisplayBuilder>? Display { get; set; }

    public CssValue<VisibilityBuilder>? Visibility { get; set; }

    public CssValue<FloatBuilder>? Float { get; set; }

    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    public CssValue<MarginBuilder>? Margin { get; set; }

    public CssValue<PaddingBuilder>? Padding { get; set; }

    public CssValue<PositionBuilder>? Position { get; set; }

    public CssValue<PositionOffsetBuilder>? PositionOffset { get; set; }

    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    public CssValue<TextStyleBuilder>? TextStyle { get; set; }

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

    public CssValue<TextColorBuilder>? TextColor { get; set; }

    public CssValue<TextDecorationBuilder>? TextDecoration { get; set; }

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

    public CssValue<BorderColorBuilder>? BorderColor { get; set; }

    public CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

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

    public CssValue<FocusRingBuilder>? FocusRing { get; set; }

    public CssValue<LinkOpacityBuilder>? LinkOpacity { get; set; }

    public CssValue<LinkOffsetBuilder>? LinkOffset { get; set; }

    public CssValue<LinkUnderlineBuilder>? LinkUnderline { get; set; }

    public CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }

    public CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

    public CssValue<TextOpacityBuilder>? TextOpacity { get; set; }

    private void CollectCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddRules(buffer, baseSelector, Display, "display");
        AddRules(buffer, baseSelector, Visibility, "visibility");
        AddRules(buffer, baseSelector, Float, "float");
        AddRules(buffer, baseSelector, VerticalAlign, "vertical-align");
        AddRules(buffer, baseSelector, TextOverflow, "text-overflow");
        AddRules(buffer, baseSelector, BoxShadow, "box-shadow");
        AddRules(buffer, baseSelector, Margin, "margin");
        AddRules(buffer, baseSelector, Padding, "padding");
        AddRules(buffer, baseSelector, Position, "position");
        AddRules(buffer, baseSelector, PositionOffset, null);
        AddRules(buffer, baseSelector, TextSize, "font-size");
        AddRules(buffer, baseSelector, TextStyle, null);
        AddRules(buffer, baseSelector, Width, "width");
        AddRules(buffer, baseSelector, MinWidth, "min-width");
        AddRules(buffer, baseSelector, MaxWidth, "max-width");
        AddRules(buffer, baseSelector, Height, "height");
        AddRules(buffer, baseSelector, MinHeight, "min-height");
        AddRules(buffer, baseSelector, MaxHeight, "max-height");
        AddRules(buffer, baseSelector, Overflow, "overflow");
        AddRules(buffer, baseSelector, OverflowX, "overflow-x");
        AddRules(buffer, baseSelector, OverflowY, "overflow-y");
        AddRules(buffer, baseSelector, ObjectFit, "object-fit");
        AddRules(buffer, baseSelector, TextAlignment, "text-align");
        AddRules(buffer, baseSelector, TextColor, "color");
        AddRules(buffer, baseSelector, TextDecoration, "text-decoration");
        AddRules(buffer, baseSelector, Flex, "flex");
        AddRules(buffer, baseSelector, Gap, "gap");
        AddRules(buffer, baseSelector, Border, "border");
        AddRules(buffer, baseSelector, Opacity, "opacity");
        AddRules(buffer, baseSelector, ZIndex, "z-index");
        AddRules(buffer, baseSelector, PointerEvents, "pointer-events");
        AddRules(buffer, baseSelector, UserSelect, "user-select");
        AddRules(buffer, baseSelector, TextTransform, "text-transform");
        AddRules(buffer, baseSelector, FontWeight, "font-weight");
        AddRules(buffer, baseSelector, FontStyle, "font-style");
        AddRules(buffer, baseSelector, LineHeight, "line-height");
        AddRules(buffer, baseSelector, TextWrap, "text-wrap");
        AddRules(buffer, baseSelector, TextBreak, "word-break");
        AddRules(buffer, baseSelector, BorderColor, "border-color");
        AddRules(buffer, baseSelector, BackgroundColor, "background-color");
        AddRules(buffer, baseSelector, Animation, "animation");
        AddRules(buffer, baseSelector, AspectRatio, "aspect-ratio");
        AddRules(buffer, baseSelector, BackdropFilter, "backdrop-filter");
        AddRules(buffer, baseSelector, BorderRadius, "border-radius");
        AddRules(buffer, baseSelector, Clearfix, null);
        AddRules(buffer, baseSelector, ClipPath, "clip-path");
        AddRules(buffer, baseSelector, Cursor, "cursor");
        AddRules(buffer, baseSelector, Filter, "filter");
        AddRules(buffer, baseSelector, Interaction, null);
        AddRules(buffer, baseSelector, ObjectPosition, "object-position");
        AddRules(buffer, baseSelector, Resize, "resize");
        AddRules(buffer, baseSelector, ScreenReader, null);
        AddRules(buffer, baseSelector, ScrollBehavior, "scroll-behavior");
        AddRules(buffer, baseSelector, StretchedLink, null);
        AddRules(buffer, baseSelector, Transform, "transform");
        AddRules(buffer, baseSelector, Transition, "transition");
        AddRules(buffer, baseSelector, Truncate, null);
        AddRules(buffer, baseSelector, FocusRing, null);
        AddRules(buffer, baseSelector, LinkOpacity, null);
        AddRules(buffer, baseSelector, LinkOffset, null);
        AddRules(buffer, baseSelector, LinkUnderline, null);
        AddRules(buffer, baseSelector, BackgroundOpacity, null);
        AddRules(buffer, baseSelector, BorderOpacity, null);
        AddRules(buffer, baseSelector, TextOpacity, null);
    }

    private static void AddRules<TBuilder>(List<ComponentCssRule> buffer, string baseSelector, CssValue<TBuilder>? value, string? fallbackProperty = null)
        where TBuilder : class, ICssBuilder
    {
        if (value is not { IsEmpty: false })
            return;

        var declarations = ConvertToDeclarations(value.Value, fallbackProperty);

        if (declarations is null)
            return;

        var resolvedSelector = ResolveSelector(baseSelector, value.Value);

        foreach (var declaration in declarations)
        {
            if (!declaration.HasContent())
                continue;

            buffer.Add(new ComponentCssRule(resolvedSelector, declaration));
        }
    }

    private static IEnumerable<string>? ConvertToDeclarations<TBuilder>(CssValue<TBuilder> value, string? fallbackProperty)
        where TBuilder : class, ICssBuilder
    {
        var styleValue = value.StyleValue;

        if (styleValue.HasContent())
            return SplitDeclarations(styleValue);

        if (fallbackProperty.IsNullOrEmpty())
            return null;

        if (!value.IsCssStyle)
            return null;

        var rawValue = value.ToString();
        if (!rawValue.HasContent())
            return null;

        return [$"{fallbackProperty}: {rawValue.Trim()}"];
    }

    private static IEnumerable<string> SplitDeclarations(string value)
    {
        var segments = value.Split(';');

        foreach (var segment in segments)
        {
            var trimmed = segment.Trim();

            if (trimmed.Length > 0)
                yield return trimmed;
        }
    }

    private static string ResolveSelector<TBuilder>(string baseSelector, CssValue<TBuilder> value)
        where TBuilder : class, ICssBuilder
    {
        var custom = value.CssSelector;

        if (custom.IsNullOrWhiteSpace())
            return baseSelector;

        var trimmed = custom.Trim();

        if (value.SelectorIsAbsolute)
            return trimmed;

        if (trimmed.Contains('&'))
            return trimmed.Replace("&", baseSelector);

        var first = trimmed[0];

        if (first == ':' || first == '.' || first == '#' || first == '[')
            return baseSelector + trimmed;

        return $"{baseSelector} {trimmed}";
    }
}
