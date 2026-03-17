using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Base for form-control semantics without a Value parameter.
/// Used by components that declare their own Value/ValueChanged (e.g. TextInput, NumericInput).
/// </summary>
public abstract class FormControlElementBase : InteractiveElement
{
    [CascadingParameter]
    private protected FieldContext? CurrentFieldContext { get; set; }

    private FieldContext? _subscribedFieldContext;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? Name { get; set; }

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

        if (ReadOnly)
            attrs["readonly"] = true;

        if (Placeholder is not null)
            attrs["placeholder"] = Placeholder;

        if (Required)
            attrs["required"] = true;

        if (AutoFocus)
            attrs["autofocus"] = true;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!ReferenceEquals(_subscribedFieldContext, CurrentFieldContext))
        {
            if (_subscribedFieldContext is not null)
                _subscribedFieldContext.StateChanged -= OnFieldContextStateChanged;

            _subscribedFieldContext = CurrentFieldContext;

            if (_subscribedFieldContext is not null)
                _subscribedFieldContext.StateChanged += OnFieldContextStateChanged;
        }
    }

    protected void ApplyFieldControlAttributes(Dictionary<string, object> attrs, bool isInvalid = false)
    {
        if (CurrentFieldContext is not null && !attrs.ContainsKey("id"))
            attrs["id"] = CurrentFieldContext.ControlId;

        bool effectiveInvalid = isInvalid || CurrentFieldContext?.IsInvalid == true;

        string? describedBy = CurrentFieldContext?.BuildDescribedBy(attrs.TryGetValue("aria-describedby", out object? existingDescribedBy)
            ? existingDescribedBy?.ToString()
            : null, effectiveInvalid);

        if (!string.IsNullOrWhiteSpace(describedBy))
            attrs["aria-describedby"] = describedBy;

        if (effectiveInvalid)
            attrs["aria-invalid"] = "true";
    }

    protected virtual Task HandleFieldContextChanged()
    {
        return RefreshOffThread();
    }

    private async void OnFieldContextStateChanged()
    {
        await HandleFieldContextChanged();
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Disabled);
        hc.Add(Name);
        hc.Add(ReadOnly);
        hc.Add(Placeholder);
        hc.Add(Required);
        hc.Add(AutoFocus);
        AddIf(ref hc, AccentColor);
        AddIf(ref hc, CaretColor);
        hc.Add(CurrentFieldContext?.ControlId);
        hc.Add(CurrentFieldContext?.DescriptionId);
        hc.Add(CurrentFieldContext?.ErrorId);
        hc.Add(CurrentFieldContext?.IsInvalid);
    }

    public override void Dispose()
    {
        if (_subscribedFieldContext is not null)
            _subscribedFieldContext.StateChanged -= OnFieldContextStateChanged;

        base.Dispose();
    }
}
