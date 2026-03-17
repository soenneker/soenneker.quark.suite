using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for reusable form-control semantics.
/// </summary>
public abstract class FormControlElement : InteractiveElement
{
    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string? Value { get; set; }

    [Parameter]
    public bool ReadOnly { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public bool Required { get; set; }

    [Parameter]
    public bool AutoFocus { get; set; }

    [Parameter]
    public CssValue<AccentColorBuilder>? AccentColor { get; set; }

    [Parameter]
    public CssValue<CaretColorBuilder>? CaretColor { get; set; }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);

        AddCss(ref sty, ref cls, AccentColor);
        AddCss(ref sty, ref cls, CaretColor);
    }

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (Disabled)
            attrs["disabled"] = true;

        if (Name is not null)
            attrs["name"] = Name;

        if (Value is not null)
            attrs["value"] = Value;

        if (ReadOnly)
            attrs["readonly"] = true;

        if (Placeholder is not null)
            attrs["placeholder"] = Placeholder;

        if (Required)
            attrs["required"] = true;

        if (AutoFocus)
            attrs["autofocus"] = true;
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Disabled);
        hc.Add(Name);
        hc.Add(Value);
        hc.Add(ReadOnly);
        hc.Add(Placeholder);
        hc.Add(Required);
        hc.Add(AutoFocus);
        AddIf(ref hc, AccentColor);
        AddIf(ref hc, CaretColor);
    }
}
