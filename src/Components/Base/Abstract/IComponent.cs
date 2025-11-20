using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
namespace Soenneker.Quark;

/// <summary>
/// Base component class that serves as the building block for all HTML elements in Quark.
/// </summary>
public interface IComponent : ICoreComponent
{
    /// <summary>
    /// Gets or sets additional CSS class names to be merged into the rendered element's <c>class</c> attribute.
    /// </summary>
    string? Class { get; set; }

    /// <summary>
    /// Gets or sets additional inline CSS declarations to be merged into the element's <c>style</c> attribute.
    /// </summary>
    string? Style { get; set; }

    /// <summary>
    /// Gets or sets the HTML <c>title</c> attribute (commonly used for native tooltips).
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// Gets or sets whether the element is hidden using the boolean HTML <c>hidden</c> attribute.
    /// </summary>
    bool Hidden { get; set; }

    /// <summary>
    /// Gets or sets the CSS <c>vertical-align</c> value to apply inline.
    /// </summary>
    CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    /// <summary>
    /// 
    /// </summary>
    CssValue<FloatBuilder>? Float { get; set; }

    /// <summary>
    /// 
    /// </summary>
    CssValue<DisplayBuilder>? Display { get; set; }


    /// <summary>
    /// 
    /// </summary>
    CssValue<VisibilityBuilder>? Visibility { get; set; }

    /// <summary>
    /// Gets or sets the margin configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    CssValue<MarginBuilder>? Margin { get; set; }

    /// <summary>
    /// Gets or sets the padding configuration. Will emit either classes or inline style based on the builder.
    /// </summary>
    CssValue<PaddingBuilder>? Padding { get; set; }

    /// <summary>
    /// Gets or sets the position/layout configuration (e.g., absolute, relative, offsets).
    /// </summary>
    CssValue<PositionBuilder>? Position { get; set; }

    /// <summary>
    /// Gets or sets the position offset configuration (e.g., top, bottom, start, end).
    /// </summary>
    CssValue<PositionOffsetBuilder>? PositionOffset { get; set; }

    /// <summary>
    /// Gets or sets the width configuration (e.g., fixed, responsive, utility classes).
    /// </summary>
    CssValue<WidthBuilder>? Width { get; set; }

    /// <summary>
    /// Gets or sets the minimum width configuration (e.g., fixed, responsive, utility classes).
    /// </summary>
    CssValue<WidthBuilder>? MinWidth { get; set; }

    /// <summary>
    /// Gets or sets the maximum width configuration (e.g., fixed, responsive, utility classes).
    /// </summary>
    CssValue<WidthBuilder>? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the height configuration (e.g., fixed, responsive, utility classes).
    /// </summary>
    CssValue<HeightBuilder>? Height { get; set; }

    /// <summary>
    /// Gets or sets the minimum height configuration (e.g., fixed, responsive, utility classes).
    /// </summary>
    CssValue<HeightBuilder>? MinHeight { get; set; }

    /// <summary>
    /// Gets or sets the maximum height configuration (e.g., fixed, responsive, utility classes).
    /// </summary>
    CssValue<HeightBuilder>? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the overflow configuration (e.g., hidden, auto, scroll).
    /// </summary>
    CssValue<OverflowBuilder>? Overflow { get; set; }

    /// <summary>
    /// Gets or sets the horizontal overflow configuration (e.g., hidden, auto, scroll).
    /// </summary>
    CssValue<OverflowBuilder>? OverflowX { get; set; }

    /// <summary>
    /// Gets or sets the vertical overflow configuration (e.g., hidden, auto, scroll).
    /// </summary>
    CssValue<OverflowBuilder>? OverflowY { get; set; }

    /// <summary>
    /// Gets or sets the flex configuration (e.g., flex direction, wrap, grow, shrink).
    /// </summary>
    CssValue<FlexBuilder>? Flex { get; set; }

    /// <summary>
    /// Gets or sets the gap configuration for flex and grid layouts.
    /// </summary>
    CssValue<GapBuilder>? Gap { get; set; }

    /// <summary>
    /// Gets or sets the opacity configuration.
    /// </summary>
    CssValue<OpacityBuilder>? Opacity { get; set; }

    /// <summary>
    /// Gets or sets the z-index configuration for stacking context.
    /// </summary>
    CssValue<ZIndexBuilder>? ZIndex { get; set; }

    /// <summary>
    /// Gets or sets the pointer events configuration (e.g., none, auto).
    /// </summary>
    CssValue<PointerEventsBuilder>? PointerEvents { get; set; }

    /// <summary>
    /// Gets or sets the user select configuration (e.g., none, text, all).
    /// </summary>
    CssValue<UserSelectBuilder>? UserSelect { get; set; }

    /// <summary>
    /// Gets or sets the background color to apply (implementation-specific mapping to classes or inline style).
    /// </summary>
    CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the border configuration.
    /// </summary>
    CssValue<BorderBuilder>? Border { get; set; }

    /// <summary>
    /// Gets or sets the border color to apply (implementation-specific mapping to classes or inline style).
    /// </summary>
    CssValue<BorderColorBuilder>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the border radius configuration.
    /// </summary>
    CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the text alignment configuration (e.g., start, center, end).
    /// </summary>
    CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    /// <summary>
    /// Gets or sets the text color configuration.
    /// </summary>
    CssValue<TextColorBuilder>? TextColor { get; set; }

    /// <summary>
    /// Invoked when the element is clicked.
    /// </summary>
    EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Invoked after the element reference (<see cref="ElementReference"/>) becomes available on first render.
    /// The <see cref="ElementReference"/> is passed to the callback.
    /// </summary>
    EventCallback<ElementReference> OnElementRefReady { get; set; }

    /// <summary>
    /// Gets or sets the animation configuration for CSS animations.
    /// </summary>
    CssValue<AnimationBuilder>? Animation { get; set; }

    /// <summary>
    /// Gets or sets the backdrop filter configuration.
    /// </summary>
    CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    /// <summary>
    /// Gets or sets the clearfix configuration.
    /// </summary>
    CssValue<ClearfixBuilder>? Clearfix { get; set; }

    /// <summary>
    /// Gets or sets the clip path configuration.
    /// </summary>
    CssValue<ClipPathBuilder>? ClipPath { get; set; }

    /// <summary>
    /// Gets or sets the CSS filter configuration.
    /// </summary>
    CssValue<FilterBuilder>? Filter { get; set; }

    /// <summary>
    /// Gets or sets the ratio configuration for aspect ratio utilities.
    /// </summary>
    CssValue<RatioBuilder>? Ratio { get; set; }

    /// <summary>
    /// Gets or sets the resize configuration.
    /// </summary>
    CssValue<ResizeBuilder>? Resize { get; set; }

    /// <summary>
    /// Gets or sets the screen reader configuration for accessibility.
    /// </summary>
    CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }

    /// <summary>
    /// Gets or sets the scroll behavior configuration.
    /// </summary>
    CssValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    /// <summary>
    /// Gets or sets the stretched link configuration.
    /// </summary>
    CssValue<StretchedLinkBuilder>? StretchedLink { get; set; }

    /// <summary>
    /// Gets or sets the CSS transform configuration.
    /// </summary>
    CssValue<TransformBuilder>? Transform { get; set; }

    /// <summary>
    /// Gets or sets the CSS transition configuration.
    /// </summary>
    CssValue<TransitionBuilder>? Transition { get; set; }

    /// <summary>
    /// Gets or sets the link opacity configuration.
    /// </summary>
    CssValue<LinkOpacityBuilder>? LinkOpacity { get; set; }

    /// <summary>
    /// Gets or sets the link offset configuration.
    /// </summary>
    CssValue<LinkOffsetBuilder>? LinkOffset { get; set; }

    /// <summary>
    /// Gets or sets the link underline configuration.
    /// </summary>
    CssValue<LinkUnderlineBuilder>? LinkUnderline { get; set; }
}
