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
    /// <summary>
    /// Gets or sets the focus ring style when the element is focused.
    /// </summary>
    [Parameter]
    public CssValue<FocusRingBuilder>? FocusRing { get; set; }

    /// <summary>
    /// Gets or sets the interaction behavior (e.g., pointer-events).
    /// </summary>
    [Parameter]
    public CssValue<InteractionBuilder>? Interaction { get; set; }

    /// <summary>
    /// Gets or sets the tab index for keyboard navigation.
    /// </summary>
    [Parameter]
    public int? TabIndex { get; set; }

    /// <summary>
    /// Gets or sets the ARIA role attribute.
    /// </summary>
    [Parameter]
    public string? Role { get; set; }

    /// <summary>
    /// Gets or sets the ARIA label for accessibility.
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the ARIA described-by attribute referencing describing element IDs.
    /// </summary>
    [Parameter]
    public string? AriaDescribedBy { get; set; }

    // -------- Interactive Events --------
    // Note: OnClick uses 'new' to shadow Component.OnClick because interactive surfaces
    // need to wire it up differently (with render gating, etc.)

    /// <summary>
    /// Gets or sets the callback invoked when the element is double-clicked.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the mouse enters the element.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseOver { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the mouse leaves the element.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseOut { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when a key is pressed while the element is focused.
    /// </summary>
    [Parameter]
    public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the element receives focus.
    /// </summary>
    [Parameter]
    public EventCallback<FocusEventArgs> OnFocus { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the element loses focus.
    /// </summary>
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

        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
            // Apply interactive-specific properties
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