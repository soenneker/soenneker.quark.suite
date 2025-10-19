using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.Blazor.Extensions.EventCallback;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

///<inheritdoc cref="IInteractiveSurfaceElement"/>
public abstract class InteractiveSurfaceElement : SurfaceElement, IInteractiveSurfaceElement
{
    [Parameter]
    public CssValue<CursorBuilder>? Cursor { get; set; }

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

    // -------- Interactive Events --------
    // Note: OnClick uses 'new' to shadow Component.OnClick because interactive surfaces
    // need to wire it up differently (with render gating, etc.)

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

    // -------- Event Handlers --------

    protected new virtual Task HandleClick(MouseEventArgs e)
    {
        return OnClick.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleDoubleClick(MouseEventArgs e)
    {
        return OnDoubleClick.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleMouseOver(MouseEventArgs e)
    {
        return OnMouseOver.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleMouseOut(MouseEventArgs e)
    {
        return OnMouseOut.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleKeyDown(KeyboardEventArgs e)
    {
        return OnKeyDown.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleFocus(FocusEventArgs e)
    {
        return OnFocus.InvokeIfHasDelegate(e);
    }

    protected virtual Task HandleBlur(FocusEventArgs e)
    {
        return OnBlur.InvokeIfHasDelegate(e);
    }

    // -------- Attribute Building Override --------

    protected override Dictionary<string, object> BuildAttributes()
    {
        var attributes = base.BuildAttributes();

        BuildClassAndStyleAttributes(attributes, (ref PooledStringBuilder cls, ref PooledStringBuilder sty) =>
        {
            // Apply interactive-specific properties
            AddCss(ref sty, ref cls, Cursor);
            AddCss(ref sty, ref cls, FocusRing);
            AddCss(ref sty, ref cls, Interaction);
        });

        // Add accessibility attributes
        if (TabIndex.HasValue)
            attributes["tabindex"] = TabIndex.Value;
        if (Role != null)
            attributes["role"] = Role;
        if (AriaLabel != null)
            attributes["aria-label"] = AriaLabel;
        if (AriaDescribedBy != null)
            attributes["aria-describedby"] = AriaDescribedBy;

        // Add interactive event handlers if they have delegates
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

        return attributes;
    }
}