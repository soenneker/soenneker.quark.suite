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
    /// <summary>Gets or sets the AccentColor utilities, including responsive and state variants.</summary>
    [Parameter]
    public CssValue<AccentColorBuilder>? AccentColor { get; set; }

    /// <summary>Gets or sets the NativeAppearance utilities, including responsive and state variants.</summary>
    [Parameter]
    public CssValue<AppearanceBuilder>? NativeAppearance { get; set; }

    /// <summary>
    /// Gets or sets the validation state: false, true, grammar, or spelling.
    /// </summary>
    [Parameter]
    public string? AriaInvalid { get; set; }

    [CascadingParameter]
    private protected FieldContext? CurrentFieldContext { get; set; }

    [CascadingParameter(Name = "FormControlSlot")]
    private protected bool IsFormControlSlot { get; set; }

    private FieldContext? _subscribedFieldContext;

    /// <summary>
    /// Gets or sets a value indicating whether disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets name.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether read only.
    /// </summary>
    [Parameter]
    public bool ReadOnly { get; set; }

    /// <summary>
    /// Gets or sets placeholder.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether required.
    /// </summary>
    [Parameter]
    public bool Required { get; set; }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);
        var preset = AppliedPresetContext;
        AddCss(ref cls, ResolvePresetValue(AccentColor, preset?.AccentColor, nameof(AccentColor)));
        AddCss(ref cls, ResolvePresetValue(NativeAppearance, preset?.NativeAppearance, nameof(NativeAppearance)));

    }

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);
        if (AriaInvalid is not null)
            attrs["aria-invalid"] = AriaInvalid;

        if (Disabled)
            attrs["disabled"] = QuarkAttributeValues.True;

        if (Name is not null)
            attrs["name"] = Name;

        if (ReadOnly)
            attrs["readonly"] = QuarkAttributeValues.True;

        if (Placeholder is not null)
            attrs["placeholder"] = Placeholder;

        if (Required)
            attrs["required"] = QuarkAttributeValues.True;

    }

    protected override void ApplyDefaultParameters()
    {
        base.ApplyDefaultParameters();

        if (IsFormControlSlot)
            DataSlot ??= "form-control";
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
        if (IsFormControlSlot)
            attrs["data-slot"] = DataSlot!;

        if (CurrentFieldContext is not null && !attrs.ContainsKey("id"))
            attrs["id"] = CurrentFieldContext.ControlId;

        var effectiveInvalid = isInvalid || CurrentFieldContext?.IsInvalid == true;

        var describedBy = CurrentFieldContext?.BuildDescribedBy(attrs.TryGetValue("aria-describedby", out var existingDescribedBy)
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

    private void OnFieldContextStateChanged()
    {
        _ = HandleFieldContextChanged();
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        AddIf(ref hc, AccentColor);
        AddIf(ref hc, NativeAppearance);

        hc.Add(AriaInvalid);

        hc.Add(Disabled);
        hc.Add(Name);
        hc.Add(ReadOnly);
        hc.Add(Placeholder);
        hc.Add(Required);
        hc.Add(CurrentFieldContext?.ControlId);
        hc.Add(CurrentFieldContext?.DescriptionId);
        hc.Add(CurrentFieldContext?.ErrorId);
        hc.Add(CurrentFieldContext?.IsInvalid);
        hc.Add(IsFormControlSlot);
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async ValueTask DisposeAsync()
    {
        if (_subscribedFieldContext is not null)
            _subscribedFieldContext.StateChanged -= OnFieldContextStateChanged;

        await base.DisposeAsync();
    }
}
