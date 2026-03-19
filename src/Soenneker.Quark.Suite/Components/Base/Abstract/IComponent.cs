using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Universal component contract for broad styling and universal interaction concerns.
/// </summary>
public interface IComponent : ICoreComponent
{
    string? Class { get; set; }
    string? Style { get; set; }
    string? Title { get; set; }
    bool Hidden { get; set; }

    CssValue<DisplayBuilder>? Display { get; set; }
    CssValue<VisibilityBuilder>? Visibility { get; set; }
    CssValue<FloatBuilder>? Float { get; set; }
    CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }
    CssValue<MarginBuilder>? Margin { get; set; }
    CssValue<PaddingBuilder>? Padding { get; set; }
    CssValue<PositionBuilder>? Position { get; set; }
    CssValue<PositionOffsetBuilder>? PositionOffset { get; set; }
    CssValue<WidthBuilder>? Width { get; set; }
    CssValue<MinWidthBuilder>? MinWidth { get; set; }
    CssValue<MaxWidthBuilder>? MaxWidth { get; set; }
    CssValue<HeightBuilder>? Height { get; set; }
    CssValue<HeightBuilder>? MinHeight { get; set; }
    CssValue<HeightBuilder>? MaxHeight { get; set; }
    CssValue<OverflowBuilder>? Overflow { get; set; }
    CssValue<OverflowBuilder>? OverflowX { get; set; }
    CssValue<OverflowBuilder>? OverflowY { get; set; }
    CssValue<FlexBuilder>? Flex { get; set; }
    CssValue<GapBuilder>? Gap { get; set; }
    CssValue<GridBuilder>? Grid { get; set; }
    CssValue<SpaceBuilder>? Space { get; set; }
    CssValue<DivideBuilder>? Divide { get; set; }
    CssValue<AlignBuilder>? AlignUtility { get; set; }
    CssValue<OpacityBuilder>? Opacity { get; set; }
    CssValue<ZIndexBuilder>? ZIndex { get; set; }
    CssValue<PointerEventsBuilder>? PointerEvents { get; set; }
    CssValue<UserSelectBuilder>? UserSelect { get; set; }
    CssValue<CursorBuilder>? Cursor { get; set; }
    CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }
    CssValue<BorderBuilder>? Border { get; set; }
    CssValue<BorderColorBuilder>? BorderColor { get; set; }
    CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }
    CssValue<BoxShadowBuilder>? BoxShadow { get; set; }
    CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }
    CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }
    CssValue<AnimationBuilder>? Animation { get; set; }
    CssValue<TransitionBuilder>? Transition { get; set; }

    EventCallback<MouseEventArgs> OnClick { get; set; }
    EventCallback<ElementReference> OnElementRefReady { get; set; }

    void Refresh();
    Task RefreshOffThread();
}
