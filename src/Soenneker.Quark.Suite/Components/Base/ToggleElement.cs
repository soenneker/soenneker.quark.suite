using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for toggle-like components with checked state.
/// </summary>
public abstract class ToggleElement : FormControlElement
{
    /// <summary>Identifies the bound model field for Blazor form validation.</summary>
    [Parameter]
    public Expression<Func<bool>>? CheckedExpression { get; set; }

    protected override FieldIdentifier? ResolveEditField() => CheckedExpression is not null ? FieldIdentifier.Create(CheckedExpression) : base.ResolveEditField();
    /// <summary>
    /// Gets or sets a value indicating whether checked.
    /// </summary>
    [Parameter]
    public bool Checked { get; set; }

    /// <summary>
    /// Gets or sets checked changed.
    /// </summary>
    [Parameter]
    public EventCallback<bool> CheckedChanged { get; set; }


    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Checked);
    }
}
