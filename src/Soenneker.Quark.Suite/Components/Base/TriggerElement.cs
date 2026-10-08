using System;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Minimal base for interactive trigger elements such as buttons, disclosure triggers, and menu activators.
/// </summary>
public abstract class TriggerElement : InteractiveElement
{
    /// <summary>
    /// Gets or sets a value indicating whether disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Disabled);
    }
}
