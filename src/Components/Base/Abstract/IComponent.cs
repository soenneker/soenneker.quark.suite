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
    /// Gets or sets the HTML <c>tabindex</c> value to control keyboard tab order.
    /// </summary>
    int? TabIndex { get; set; }

    /// <summary>
    /// Gets or sets whether the element is hidden using the boolean HTML <c>hidden</c> attribute.
    /// </summary>
    bool Hidden { get; set; }

    /// <summary>
    /// Gets or sets the CSS <c>vertical-align</c> value to apply inline.
    /// </summary>
    CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    /// <summary>
    /// Gets or sets the CSS <c>text-overflow</c> value to apply inline.
    /// </summary>
    CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    /// <summary>
    /// Gets or sets the CSS <c>box-shadow</c> value to apply inline.
    /// </summary>
    CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

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
    CssValue<PositionOffsetBuilder>? Offset { get; set; }

    /// <summary>
    /// Gets or sets the text size configuration (e.g., font-size utilities).
    /// </summary>
    CssValue<TextSizeBuilder>? TextSize { get; set; }

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
    /// Gets or sets the object-fit configuration for replaced content (e.g., images, video).
    /// </summary>
    CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    /// <summary>
    /// Gets or sets the CSS <c>text-align</c> value to apply inline.
    /// </summary>
    CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    /// <summary>
    /// Gets or sets the CSS <c>text-decoration-line</c> value to apply inline.
    /// </summary>
    CssValue<TextDecorationBuilder>? TextDecorationLine { get; set; }

    /// <summary>
    /// Gets or sets the CSS <c>text-decoration</c> value to apply inline.
    /// </summary>
    CssValue<TextDecorationBuilder>? TextDecorationCss { get; set; }

    /// <summary>
    /// Gets or sets the flex configuration (e.g., flex direction, wrap, grow, shrink).
    /// </summary>
    CssValue<FlexBuilder>? Flex { get; set; }

    /// <summary>
    /// Gets or sets the gap configuration for flex and grid layouts.
    /// </summary>
    CssValue<GapBuilder>? Gap { get; set; }

    /// <summary>
    /// Gets or sets the border configuration (e.g., width, style, color).
    /// </summary>
    CssValue<BorderBuilder>? Border { get; set; }

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
    /// Gets or sets the text transform configuration (e.g., uppercase, lowercase, capitalize).
    /// </summary>
    CssValue<TextTransformBuilder>? TextTransform { get; set; }

    /// <summary>
    /// Gets or sets the font weight configuration (e.g., normal, bold, 100-900).
    /// </summary>
    CssValue<FontWeightBuilder>? FontWeight { get; set; }

    /// <summary>
    /// Gets or sets the font style configuration (e.g., normal, italic, oblique).
    /// </summary>
    CssValue<FontStyleBuilder>? FontStyle { get; set; }

    /// <summary>
    /// Gets or sets the line height configuration.
    /// </summary>
    CssValue<LineHeightBuilder>? LineHeight { get; set; }

    /// <summary>
    /// Gets or sets the text wrap configuration (e.g., wrap, nowrap, balance).
    /// </summary>
    CssValue<TextWrapBuilder>? TextWrap { get; set; }

    /// <summary>
    /// Gets or sets the text break configuration (e.g., normal, break-all, break-word).
    /// </summary>
    CssValue<TextBreakBuilder>? TextBreak { get; set; }

    /// <summary>
    /// Invoked when the element is clicked.
    /// </summary>
    EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Invoked when the element is double-clicked.
    /// </summary>
    EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

    /// <summary>
    /// Invoked when the pointer moves over the element.
    /// </summary>
    EventCallback<MouseEventArgs> OnMouseOver { get; set; }

    /// <summary>
    /// Invoked when the pointer leaves the element.
    /// </summary>
    EventCallback<MouseEventArgs> OnMouseOut { get; set; }

    /// <summary>
    /// Invoked when a key is pressed while the element has focus.
    /// </summary>
    EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

    /// <summary>
    /// Invoked when the element receives focus.
    /// </summary>
    EventCallback<FocusEventArgs> OnFocus { get; set; }

    /// <summary>
    /// Invoked when the element loses focus.
    /// </summary>
    EventCallback<FocusEventArgs> OnBlur { get; set; }

    /// <summary>
    /// Invoked after the element reference (<see cref="ElementReference"/>) becomes available on first render.
    /// The <see cref="ElementReference"/> is passed to the callback.
    /// </summary>
    EventCallback<ElementReference> OnElementRefReady { get; set; }

    /// <summary>
    /// Gets or sets the text color to apply (implementation-specific mapping to classes or inline style).
    /// </summary>
    CssValue<TextColorBuilder>? TextColor { get; set; }

    /// <summary>
    /// Gets or sets the background color to apply (implementation-specific mapping to classes or inline style).
    /// </summary>
    CssValue<ColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the animation configuration for CSS animations.
    /// </summary>
    CssValue<AnimationBuilder>? Animation { get; set; }

    /// <summary>
    /// Gets or sets the aspect ratio configuration.
    /// </summary>
    CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    /// <summary>
    /// Gets or sets the backdrop filter configuration.
    /// </summary>
    CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    /// <summary>
    /// Gets or sets the border radius configuration.
    /// </summary>
    CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the clearfix configuration.
    /// </summary>
    CssValue<ClearfixBuilder>? Clearfix { get; set; }

    /// <summary>
    /// Gets or sets the clip path configuration.
    /// </summary>
    CssValue<ClipPathBuilder>? ClipPath { get; set; }

    /// <summary>
    /// Gets or sets the cursor configuration.
    /// </summary>
    CssValue<CursorBuilder>? Cursor { get; set; }

    /// <summary>
    /// Gets or sets the CSS filter configuration.
    /// </summary>
    CssValue<FilterBuilder>? Filter { get; set; }

    /// <summary>
    /// Gets or sets the interaction configuration for hover, focus, and active states.
    /// </summary>
    CssValue<InteractionBuilder>? Interaction { get; set; }

    /// <summary>
    /// Gets or sets the object position configuration for replaced content.
    /// </summary>
    CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

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
    /// Gets or sets the text truncation configuration.
    /// </summary>
    CssValue<TruncateBuilder>? Truncate { get; set; }

    /// <summary>
    /// Gets or sets the ARIA <c>role</c> attribute for accessibility semantics.
    /// </summary>
    string? Role { get; set; }

    /// <summary>
    /// Gets or sets the ARIA <c>aria-label</c> attribute to provide an accessible label.
    /// </summary>
    string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the ARIA <c>aria-describedby</c> attribute to reference descriptive content.
    /// </summary>
    string? AriaDescribedBy { get; set; }
}
