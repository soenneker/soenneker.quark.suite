using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Represents CSS styling options for a component, allowing configuration of various CSS properties
/// that can be applied to the component's selector in theme generation.
/// </summary>
public class ComponentOptions
{
    /// <summary>Gets or sets the CSS selector for this component (e.g., "a", "i", ":root").</summary>
    public string Selector { get; set; } = ":root";

    /// <summary>
    /// Gets all CSS rules for this component based on the configured properties.
    /// </summary>
    internal virtual IEnumerable<ComponentCssRule> GetCssRules()
    {
        if (Selector.IsNullOrWhiteSpace())
            return [];

        var buffer = new List<ComponentCssRule>(32);
        CollectCssRules(buffer, Selector);
        return buffer;
    }

    /// <summary>
    /// Gets or sets the CSS display configuration.
    /// </summary>
    public CssValue<DisplayBuilder>? Display { get; set; }

    /// <summary>
    /// Gets or sets the CSS visibility configuration.
    /// </summary>
    public CssValue<VisibilityBuilder>? Visibility { get; set; }

    /// <summary>
    /// Gets or sets the CSS float configuration.
    /// </summary>
    public CssValue<FloatBuilder>? Float { get; set; }

    /// <summary>
    /// Gets or sets the CSS vertical-align configuration.
    /// </summary>
    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-overflow configuration.
    /// </summary>
    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    /// <summary>
    /// Gets or sets the CSS box-shadow configuration.
    /// </summary>
    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    /// <summary>
    /// Gets or sets the CSS margin configuration.
    /// </summary>
    public CssValue<MarginBuilder>? Margin { get; set; }

    /// <summary>
    /// Gets or sets the CSS padding configuration.
    /// </summary>
    public CssValue<PaddingBuilder>? Padding { get; set; }

    /// <summary>
    /// Gets or sets the CSS position configuration.
    /// </summary>
    public CssValue<PositionBuilder>? Position { get; set; }

    /// <summary>
    /// Gets or sets the CSS position offset configuration.
    /// </summary>
    public CssValue<PositionOffsetBuilder>? PositionOffset { get; set; }

    /// <summary>
    /// Gets or sets the CSS text size (font-size) configuration.
    /// </summary>
    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    /// <summary>
    /// Gets or sets the CSS text style configuration.
    /// </summary>
    public CssValue<TextStyleBuilder>? TextStyle { get; set; }

    /// <summary>
    /// Gets or sets the CSS width configuration.
    /// </summary>
    public CssValue<WidthBuilder>? Width { get; set; }

    /// <summary>
    /// Gets or sets the CSS minimum width configuration.
    /// </summary>
    public CssValue<WidthBuilder>? MinWidth { get; set; }

    /// <summary>
    /// Gets or sets the CSS maximum width configuration.
    /// </summary>
    public CssValue<WidthBuilder>? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the CSS height configuration.
    /// </summary>
    public CssValue<HeightBuilder>? Height { get; set; }

    /// <summary>
    /// Gets or sets the CSS minimum height configuration.
    /// </summary>
    public CssValue<HeightBuilder>? MinHeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS maximum height configuration.
    /// </summary>
    public CssValue<HeightBuilder>? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS overflow configuration.
    /// </summary>
    public CssValue<OverflowBuilder>? Overflow { get; set; }

    /// <summary>
    /// Gets or sets the CSS horizontal overflow configuration.
    /// </summary>
    public CssValue<OverflowBuilder>? OverflowX { get; set; }

    /// <summary>
    /// Gets or sets the CSS vertical overflow configuration.
    /// </summary>
    public CssValue<OverflowBuilder>? OverflowY { get; set; }

    /// <summary>
    /// Gets or sets the CSS object-fit configuration.
    /// </summary>
    public CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    /// <summary>
    /// Gets or sets the CSS text alignment configuration.
    /// </summary>
    public CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    /// <summary>
    /// Gets or sets the CSS text color configuration.
    /// </summary>
    public CssValue<TextColorBuilder>? TextColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS text decoration configuration.
    /// </summary>
    public CssValue<TextDecorationBuilder>? TextDecoration { get; set; }

    /// <summary>
    /// Gets or sets the CSS flex configuration.
    /// </summary>
    public CssValue<FlexBuilder>? Flex { get; set; }

    /// <summary>
    /// Gets or sets the CSS gap configuration.
    /// </summary>
    public CssValue<GapBuilder>? Gap { get; set; }

    /// <summary>
    /// Gets or sets the CSS border configuration.
    /// </summary>
    public CssValue<BorderBuilder>? Border { get; set; }

    /// <summary>
    /// Gets or sets the CSS opacity configuration.
    /// </summary>
    public CssValue<OpacityBuilder>? Opacity { get; set; }

    /// <summary>
    /// Gets or sets the CSS z-index configuration.
    /// </summary>
    public CssValue<ZIndexBuilder>? ZIndex { get; set; }

    /// <summary>
    /// Gets or sets the CSS pointer-events configuration.
    /// </summary>
    public CssValue<PointerEventsBuilder>? PointerEvents { get; set; }

    /// <summary>
    /// Gets or sets the CSS user-select configuration.
    /// </summary>
    public CssValue<UserSelectBuilder>? UserSelect { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-transform configuration.
    /// </summary>
    public CssValue<TextTransformBuilder>? TextTransform { get; set; }

    /// <summary>
    /// Gets or sets the CSS font-weight configuration.
    /// </summary>
    public CssValue<FontWeightBuilder>? FontWeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS font-style configuration.
    /// </summary>
    public CssValue<FontStyleBuilder>? FontStyle { get; set; }

    /// <summary>
    /// Gets or sets the CSS line-height configuration.
    /// </summary>
    public CssValue<LineHeightBuilder>? LineHeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-wrap configuration.
    /// </summary>
    public CssValue<TextWrapBuilder>? TextWrap { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-break (word-break) configuration.
    /// </summary>
    public CssValue<TextBreakBuilder>? TextBreak { get; set; }

    /// <summary>
    /// Gets or sets the CSS border-color configuration.
    /// </summary>
    public CssValue<BorderColorBuilder>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS background-color configuration.
    /// </summary>
    public CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS animation configuration.
    /// </summary>
    public CssValue<AnimationBuilder>? Animation { get; set; }

    /// <summary>
    /// Gets or sets the CSS aspect-ratio configuration.
    /// </summary>
    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    /// <summary>
    /// Gets or sets the CSS backdrop-filter configuration.
    /// </summary>
    public CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    /// <summary>
    /// Gets or sets the CSS border-radius configuration.
    /// </summary>
    public CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the CSS clearfix configuration.
    /// </summary>
    public CssValue<ClearfixBuilder>? Clearfix { get; set; }

    /// <summary>
    /// Gets or sets the CSS clip-path configuration.
    /// </summary>
    public CssValue<ClipPathBuilder>? ClipPath { get; set; }

    /// <summary>
    /// Gets or sets the CSS cursor configuration.
    /// </summary>
    public CssValue<CursorBuilder>? Cursor { get; set; }

    /// <summary>
    /// Gets or sets the CSS filter configuration.
    /// </summary>
    public CssValue<FilterBuilder>? Filter { get; set; }

    /// <summary>
    /// Gets or sets the CSS interaction configuration.
    /// </summary>
    public CssValue<InteractionBuilder>? Interaction { get; set; }

    /// <summary>
    /// Gets or sets the CSS object-position configuration.
    /// </summary>
    public CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    /// <summary>
    /// Gets or sets the CSS resize configuration.
    /// </summary>
    public CssValue<ResizeBuilder>? Resize { get; set; }

    /// <summary>
    /// Gets or sets the CSS screen-reader configuration.
    /// </summary>
    public CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }

    /// <summary>
    /// Gets or sets the CSS scroll-behavior configuration.
    /// </summary>
    public CssValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    /// <summary>
    /// Gets or sets the CSS stretched-link configuration.
    /// </summary>
    public CssValue<StretchedLinkBuilder>? StretchedLink { get; set; }

    /// <summary>
    /// Gets or sets the CSS transform configuration.
    /// </summary>
    public CssValue<TransformBuilder>? Transform { get; set; }

    /// <summary>
    /// Gets or sets the CSS transition configuration.
    /// </summary>
    public CssValue<TransitionBuilder>? Transition { get; set; }

    /// <summary>
    /// Gets or sets the CSS truncate configuration.
    /// </summary>
    public CssValue<TruncateBuilder>? Truncate { get; set; }

    /// <summary>
    /// Gets or sets the CSS focus-ring configuration.
    /// </summary>
    public CssValue<FocusRingBuilder>? FocusRing { get; set; }

    /// <summary>
    /// Gets or sets the CSS link-opacity configuration.
    /// </summary>
    public CssValue<LinkOpacityBuilder>? LinkOpacity { get; set; }

    /// <summary>
    /// Gets or sets the CSS link-offset configuration.
    /// </summary>
    public CssValue<LinkOffsetBuilder>? LinkOffset { get; set; }

    /// <summary>
    /// Gets or sets the CSS link-underline configuration.
    /// </summary>
    public CssValue<LinkUnderlineBuilder>? LinkUnderline { get; set; }

    /// <summary>
    /// Gets or sets the CSS background-opacity configuration.
    /// </summary>
    public CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }

    /// <summary>
    /// Gets or sets the CSS border-opacity configuration.
    /// </summary>
    public CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-opacity configuration.
    /// </summary>
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

        IEnumerable<string>? declarations = ConvertToDeclarations(value.Value, fallbackProperty);

        if (declarations is null)
            return;

        string resolvedSelector = ResolveSelector(baseSelector, value.Value);

        foreach (string declaration in declarations)
        {
            if (!declaration.HasContent())
                continue;

            buffer.Add(new ComponentCssRule(resolvedSelector, declaration));
        }
    }

    private static IEnumerable<string>? ConvertToDeclarations<TBuilder>(CssValue<TBuilder> value, string? fallbackProperty)
        where TBuilder : class, ICssBuilder
    {
        string styleValue = value.StyleValue;

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
        string[] segments = value.Split(';');

        foreach (string segment in segments)
        {
            string trimmed = segment.Trim();

            if (trimmed.Length > 0)
                yield return trimmed;
        }
    }

    private static string ResolveSelector<TBuilder>(string baseSelector, CssValue<TBuilder> value)
        where TBuilder : class, ICssBuilder
    {
        string? custom = value.CssSelector;

        if (custom.IsNullOrWhiteSpace())
            return baseSelector;

        string trimmed = custom.Trim();

        if (value.SelectorIsAbsolute)
            return trimmed;

        if (trimmed.Contains('&'))
            return trimmed.Replace("&", baseSelector);

        char first = trimmed[0];

        if (first == ':' || first == '.' || first == '#' || first == '[')
            return baseSelector + trimmed;

        return $"{baseSelector} {trimmed}";
    }
}
