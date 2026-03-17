using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Minimal base for interactive trigger elements such as buttons, disclosure triggers, and menu activators.
/// </summary>
public abstract class TriggerElement : InteractiveElement
{
    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public bool AutoFocus { get; set; }

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (AutoFocus)
            attrs["autofocus"] = true;
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Disabled);
        hc.Add(AutoFocus);
    }
}
