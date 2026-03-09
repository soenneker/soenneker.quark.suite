using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.Blazor.Extensions.EventCallback;

namespace Soenneker.Quark;

///<inheritdoc cref="IElement"/>
public abstract class Element : Component, IElement
{
    /// <summary>
    /// Gets or sets the child content to be rendered within the element.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    [Parameter]
    public CssValue<TextDecorationBuilder>? TextDecoration { get; set; }

    [Parameter]
    public CssValue<TextTransformBuilder>? TextTransform { get; set; }

    [Parameter]
    public CssValue<FontWeightBuilder>? FontWeight { get; set; }

    [Parameter]
    public CssValue<FontStyleBuilder>? FontStyle { get; set; }

    [Parameter]
    public CssValue<LineHeightBuilder>? LineHeight { get; set; }

    [Parameter]
    public CssValue<TextWrapBuilder>? TextWrap { get; set; }

    [Parameter]
    public CssValue<TextBreakBuilder>? TextBreak { get; set; }

    [Parameter]
    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    [Parameter]
    public CssValue<TruncateBuilder>? Truncate { get; set; }

    [Parameter]
    public CssValue<TextStyleBuilder>? TextStyle { get; set; }

    [Parameter]
    public CssValue<TextBackgroundBuilder>? TextBackground { get; set; }

    [Parameter]
    public CssValue<TextOpacityBuilder>? TextOpacity { get; set; }

    [Parameter]
    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    [Parameter]
    public CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }

    [Parameter]
    public CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

    [Parameter]
    public string Tag { get; set; } = "div";

    // Interactive (from InteractiveSurfaceElement)
    [Parameter]
    public CssValue<FocusRingBuilder>? FocusRing { get; set; }

    [Parameter]
    public CssValue<InteractionBuilder>? Interaction { get; set; }

    [Parameter]
    public int? TabIndex { get; set; }

    [Parameter]
    public string? Role { get; set; }

    [Parameter]
    public string? AriaLabel { get; set; }

    [Parameter]
    public string? AriaDescribedBy { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseOver { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseOut { get; set; }

    [Parameter]
    public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

    [Parameter]
    public EventCallback<FocusEventArgs> OnFocus { get; set; }

    [Parameter]
    public EventCallback<FocusEventArgs> OnBlur { get; set; }

    // Toggle (from ToggleElement)
    [Parameter]
    public bool Checked { get; set; }

    [Parameter]
    public EventCallback<bool> CheckedChanged { get; set; }

    [Parameter]
    public Expression<Func<bool>>? CheckedExpression { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string? Value { get; set; }

    // Input (from InputSurfaceElement)
    [Parameter]
    public bool ReadOnly { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public bool Required { get; set; }

    [Parameter]
    public bool AutoFocus { get; set; }

    // Visual media (from VisualMediaElement)
    [Parameter]
    public string? Source { get; set; }

    [Parameter]
    public string? Alt { get; set; }

    [Parameter]
    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    [Parameter]
    public CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    [Parameter]
    public CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    [Parameter]
    public bool Lazy { get; set; }

    // Overlay (from Overlay)
    [Parameter]
    public bool Visible { get; set; }

    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    [Parameter]
    public bool ShowBackdrop { get; set; } = true;

    [Parameter]
    public bool CloseOnBackdropClick { get; set; } = true;

    [Parameter]
    public bool CloseOnEscape { get; set; } = true;

    [Parameter]
    public EventCallback OnShow { get; set; }

    [Parameter]
    public EventCallback OnHide { get; set; }

    private bool _lastVisible;
    private RenderFragment? _lastChildContentRef;
    private bool _childContentChanged;

    protected override void BuildAttributesCore(Dictionary<string, object> attributes)
    {
        base.BuildAttributesCore(attributes);

        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
            AddCss(ref sty, ref cls, TextSize);
            AddCss(ref sty, ref cls, TextDecoration);
            AddCss(ref sty, ref cls, TextTransform);
            AddCss(ref sty, ref cls, FontWeight);
            AddCss(ref sty, ref cls, FontStyle);
            AddCss(ref sty, ref cls, LineHeight);
            AddCss(ref sty, ref cls, TextWrap);
            AddCss(ref sty, ref cls, TextBreak);
            AddCss(ref sty, ref cls, TextOverflow);
            AddCss(ref sty, ref cls, Truncate);
            AddCss(ref sty, ref cls, TextStyle);
            AddCss(ref sty, ref cls, TextBackground);
            AddCss(ref sty, ref cls, TextOpacity);
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, BackgroundOpacity);
            AddCss(ref sty, ref cls, BorderOpacity);
            AddCss(ref sty, ref cls, FocusRing);
            AddCss(ref sty, ref cls, Interaction);
        });

        if (TabIndex.HasValue)
            attributes["tabindex"] = TabIndex.Value;
        if (Role is not null)
            attributes["role"] = Role;
        if (AriaLabel is not null)
            attributes["aria-label"] = AriaLabel;
        if (AriaDescribedBy is not null)
            attributes["aria-describedby"] = AriaDescribedBy;
        if (OnDoubleClick.HasDelegate)
            attributes["ondblclick"] = OnDoubleClick;
        if (OnMouseOver.HasDelegate)
            attributes["onmouseover"] = OnMouseOver;
        if (OnMouseOut.HasDelegate)
            attributes["onmouseout"] = OnMouseOut;
        if (OnKeyDown.HasDelegate)
            attributes["onkeydown"] = OnKeyDown;
        if (OnFocus.HasDelegate)
            attributes["onfocus"] = OnFocus;
        if (OnBlur.HasDelegate)
            attributes["onblur"] = OnBlur;
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        AddIf(ref hc, TextSize);
        AddIf(ref hc, TextDecoration);
        AddIf(ref hc, TextTransform);
        AddIf(ref hc, FontWeight);
        AddIf(ref hc, FontStyle);
        AddIf(ref hc, LineHeight);
        AddIf(ref hc, TextWrap);
        AddIf(ref hc, TextBreak);
        AddIf(ref hc, TextOverflow);
        AddIf(ref hc, Truncate);
        AddIf(ref hc, TextStyle);
        AddIf(ref hc, TextBackground);
        AddIf(ref hc, TextOpacity);
        AddIf(ref hc, BoxShadow);
        AddIf(ref hc, BackgroundOpacity);
        AddIf(ref hc, BorderOpacity);
        hc.Add(Tag);
        AddIf(ref hc, FocusRing);
        AddIf(ref hc, Interaction);
        hc.Add(TabIndex);
        hc.Add(Role);
        hc.Add(AriaLabel);
        hc.Add(AriaDescribedBy);
        hc.Add(Checked);
        hc.Add(Disabled);
        hc.Add(Name);
        hc.Add(Value);
        hc.Add(ReadOnly);
        hc.Add(Placeholder);
        hc.Add(Required);
        hc.Add(AutoFocus);
        hc.Add(Source);
        hc.Add(Alt);
        AddIf(ref hc, AspectRatio);
        AddIf(ref hc, ObjectFit);
        AddIf(ref hc, ObjectPosition);
        hc.Add(Lazy);
        hc.Add(Visible);
        hc.Add(ShowBackdrop);
        hc.Add(CloseOnBackdropClick);
        hc.Add(CloseOnEscape);
        hc.Add(OnDoubleClick.HasDelegate);
        hc.Add(OnMouseOver.HasDelegate);
        hc.Add(OnMouseOut.HasDelegate);
        hc.Add(OnKeyDown.HasDelegate);
        hc.Add(OnFocus.HasDelegate);
        hc.Add(OnBlur.HasDelegate);
        hc.Add(OnClick.HasDelegate);
    }

    protected virtual Task HandleDoubleClick(MouseEventArgs e) => OnDoubleClick.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOver(MouseEventArgs e) => OnMouseOver.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOut(MouseEventArgs e) => OnMouseOut.InvokeIfHasDelegate(e);
    protected virtual Task HandleKeyDown(KeyboardEventArgs e) => OnKeyDown.InvokeIfHasDelegate(e);
    protected virtual Task HandleFocus(FocusEventArgs e) => OnFocus.InvokeIfHasDelegate(e);
    protected virtual Task HandleBlur(FocusEventArgs e) => OnBlur.InvokeIfHasDelegate(e);

    protected override void OnParametersSet()
    {
        var cc = ChildContent;
        _childContentChanged = !ReferenceEquals(cc, _lastChildContentRef);
        _lastChildContentRef = cc;

        base.OnParametersSet();
    }

    protected override bool ShouldRender()
    {
        if (QuarkOptions.AlwaysRender)
            return true;

        if (Visible != _lastVisible)
        {
            _lastVisible = Visible;
            return true;
        }

        if (_childContentChanged)
        {
            // Consume the change so subsequent render-gate checks don’t keep firing
            _childContentChanged = false;
            return true;
        }

        return base.ShouldRender();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_childContentChanged)
        {
            _lastChildContentRef = ChildContent;
            _childContentChanged = false;
        }

        await base.OnAfterRenderAsync(firstRender);
    }
}