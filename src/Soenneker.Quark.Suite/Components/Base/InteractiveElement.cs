using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.Blazor.Extensions.EventCallback;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Focused interaction base for keyboard, mouse, and focus concerns beyond universal click handling.
/// </summary>
public abstract class InteractiveElement : Element
{
    [Parameter]
    public CssValue<RingOffsetBuilder>? RingOffset { get; set; }

    [Parameter]
    public CssValue<InteractionBuilder>? Interaction { get; set; }

    [Parameter]
    public CssValue<OutlineStyleBuilder>? OutlineStyle { get; set; }

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

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);

        AddCss(ref sty, ref cls, RingOffset);
        AddCss(ref sty, ref cls, Interaction);
        AddCss(ref sty, ref cls, OutlineStyle);
    }

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (OnDoubleClick.HasDelegate)
            attrs["ondblclick"] = OnDoubleClick;

        if (OnMouseOver.HasDelegate)
            attrs["onmouseover"] = OnMouseOver;

        if (OnMouseOut.HasDelegate)
            attrs["onmouseout"] = OnMouseOut;

        if (OnKeyDown.HasDelegate)
            attrs["onkeydown"] = OnKeyDown;

        if (OnFocus.HasDelegate)
            attrs["onfocus"] = OnFocus;

        if (OnBlur.HasDelegate)
            attrs["onblur"] = OnBlur;
    }

    protected virtual Task HandleDoubleClick(MouseEventArgs e) => OnDoubleClick.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOver(MouseEventArgs e) => OnMouseOver.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOut(MouseEventArgs e) => OnMouseOut.InvokeIfHasDelegate(e);
    protected virtual Task HandleKeyDown(KeyboardEventArgs e) => OnKeyDown.InvokeIfHasDelegate(e);
    protected virtual Task HandleFocus(FocusEventArgs e) => OnFocus.InvokeIfHasDelegate(e);
    protected virtual Task HandleBlur(FocusEventArgs e) => OnBlur.InvokeIfHasDelegate(e);

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        AddIf(ref hc, RingOffset);
        AddIf(ref hc, Interaction);
        AddIf(ref hc, OutlineStyle);
        hc.Add(OnDoubleClick.HasDelegate);
        hc.Add(OnMouseOver.HasDelegate);
        hc.Add(OnMouseOut.HasDelegate);
        hc.Add(OnKeyDown.HasDelegate);
        hc.Add(OnFocus.HasDelegate);
        hc.Add(OnBlur.HasDelegate);
    }
}
