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
    /// <summary>
    /// Gets or sets whether the interactive element is unavailable.
    /// </summary>
    [Parameter]
    public bool? AriaDisabled { get; set; }

    /// <summary>
    /// Gets or sets whether the controlled content is expanded.
    /// </summary>
    [Parameter]
    public bool? AriaExpanded { get; set; }

    /// <summary>
    /// Gets or sets the popup type: false, true, menu, listbox, tree, grid, or dialog.
    /// </summary>
    [Parameter]
    public string? AriaHasPopup { get; set; }


    /// <summary>
    /// Gets or sets on click.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Gets or sets on double click.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnDoubleClick { get; set; }

    /// <summary>
    /// Gets or sets on mouse over.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseOver { get; set; }

    /// <summary>
    /// Gets or sets on mouse out.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseOut { get; set; }

    /// <summary>
    /// Gets or sets on key down.
    /// </summary>
    [Parameter]
    public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

    /// <summary>
    /// Gets or sets on focus.
    /// </summary>
    [Parameter]
    public EventCallback<FocusEventArgs> OnFocus { get; set; }

    /// <summary>
    /// Gets or sets on blur.
    /// </summary>
    [Parameter]
    public EventCallback<FocusEventArgs> OnBlur { get; set; }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);
    }

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);
        if (AriaDisabled.HasValue)
            attrs["aria-disabled"] = AriaDisabled.Value ? "true" : "false";

        if (AriaExpanded.HasValue)
            attrs["aria-expanded"] = AriaExpanded.Value ? "true" : "false";

        if (AriaHasPopup is not null)
            attrs["aria-haspopup"] = AriaHasPopup;


        if (OnDoubleClick.HasDelegate)
            SetEventAttribute(attrs, "ondblclick", OnDoubleClick);

        if (OnMouseOver.HasDelegate)
            SetEventAttribute(attrs, "onmouseover", OnMouseOver);

        if (OnMouseOut.HasDelegate)
            SetEventAttribute(attrs, "onmouseout", OnMouseOut);

        if (OnKeyDown.HasDelegate)
            SetEventAttribute(attrs, "onkeydown", OnKeyDown);

        if (OnFocus.HasDelegate)
            SetEventAttribute(attrs, "onfocus", OnFocus);

        if (OnBlur.HasDelegate)
            SetEventAttribute(attrs, "onblur", OnBlur);
    }

    protected virtual Task HandleClick(MouseEventArgs e) => OnClick.InvokeIfHasDelegate(e);
    protected virtual Task HandleDoubleClick(MouseEventArgs e) => OnDoubleClick.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOver(MouseEventArgs e) => OnMouseOver.InvokeIfHasDelegate(e);
    protected virtual Task HandleMouseOut(MouseEventArgs e) => OnMouseOut.InvokeIfHasDelegate(e);
    protected virtual Task HandleKeyDown(KeyboardEventArgs e) => OnKeyDown.InvokeIfHasDelegate(e);
    protected virtual Task HandleFocus(FocusEventArgs e) => OnFocus.InvokeIfHasDelegate(e);
    protected virtual Task HandleBlur(FocusEventArgs e) => OnBlur.InvokeIfHasDelegate(e);

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        hc.Add(AriaDisabled);
        hc.Add(AriaExpanded);
        hc.Add(AriaHasPopup);


        hc.Add(OnClick.HasDelegate);
        hc.Add(OnDoubleClick.HasDelegate);
        hc.Add(OnMouseOver.HasDelegate);
        hc.Add(OnMouseOut.HasDelegate);
        hc.Add(OnKeyDown.HasDelegate);
        hc.Add(OnFocus.HasDelegate);
        hc.Add(OnBlur.HasDelegate);
    }
}
