using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.Blazor.Extensions.EventCallback;

namespace Soenneker.Quark;

public abstract class InteractiveSurfaceElement : SurfaceElement, IInteractiveSurfaceElement
{
    [Parameter] public CssValue<FocusRingBuilder>? FocusRing { get; set; }
    [Parameter] public CssValue<InteractionBuilder>? Interaction { get; set; }

    [Parameter] public int? TabIndex { get; set; }
    [Parameter] public string? Role { get; set; }
    [Parameter] public string? AriaLabel { get; set; }
    [Parameter] public string? AriaDescribedBy { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnDoubleClick { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnMouseOver { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnMouseOut { get; set; }
    [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }

    // If SurfaceElement/Component already has OnClick, keep using it.
    // If you truly must shadow, ensure the property exists here; your snippet doesn’t show it.
    // Otherwise `new HandleClick` is pointless / confusing.

    protected new virtual Task HandleClick(MouseEventArgs e) => OnClick.InvokeIfHasDelegate(e);
    protected virtual Task HandleDoubleClick(MouseEventArgs e) => OnDoubleClick.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOver(MouseEventArgs e) => OnMouseOver.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOut(MouseEventArgs e) => OnMouseOut.InvokeIfHasDelegate(e);
    protected virtual Task HandleKeyDown(KeyboardEventArgs e) => OnKeyDown.InvokeIfHasDelegate(e);
    protected virtual Task HandleFocus(FocusEventArgs e) => OnFocus.InvokeIfHasDelegate(e);
    protected virtual Task HandleBlur(FocusEventArgs e) => OnBlur.InvokeIfHasDelegate(e);

    protected override void BuildAttributesCore(Dictionary<string, object> attributes)
    {
        base.BuildAttributesCore(attributes);

        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
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

        // Event attributes
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
        AddIf(ref hc, FocusRing);
        AddIf(ref hc, Interaction);

        hc.Add(TabIndex);
        hc.Add(Role);
        hc.Add(AriaLabel);
        hc.Add(AriaDescribedBy);

        // For EventCallbacks, hash only "presence", not the delegate itself.
        hc.Add(OnDoubleClick.HasDelegate);
        hc.Add(OnMouseOver.HasDelegate);
        hc.Add(OnMouseOut.HasDelegate);
        hc.Add(OnKeyDown.HasDelegate);
        hc.Add(OnFocus.HasDelegate);
        hc.Add(OnBlur.HasDelegate);

        // If click wiring differs, include click delegate presence too:
        hc.Add(OnClick.HasDelegate);
    }
}
