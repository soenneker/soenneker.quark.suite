using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for toggle-like components with checked state.
/// </summary>
public abstract class ToggleElement : FormControlElement
{
    [Parameter]
    public bool Checked { get; set; }

    [Parameter]
    public EventCallback<bool> CheckedChanged { get; set; }

    [Parameter]
    public Expression<Func<bool>>? CheckedExpression { get; set; }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Checked);
        hc.Add(CheckedExpression);
    }
}
