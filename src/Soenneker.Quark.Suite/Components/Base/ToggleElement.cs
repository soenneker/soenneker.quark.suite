using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for toggle-like components with checked state.
/// </summary>
public abstract class ToggleElement : FormControlElement
{
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

    /// <summary>
    /// Gets or sets checked expression.
    /// </summary>
    [Parameter]
    public Expression<Func<bool>>? CheckedExpression { get; set; }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Checked);
        hc.Add(CheckedExpression);
    }
}
