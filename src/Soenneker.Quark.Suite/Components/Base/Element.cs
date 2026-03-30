using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="IElement"/>
public abstract class Element : Component, IElement
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string Tag { get; set; } = "div";

    [Parameter]
    public int? TabIndex { get; set; }

    [Parameter]
    public string? Role { get; set; }

    [Parameter]
    public string? AriaLabel { get; set; }

    [Parameter]
    public string? AriaDescribedBy { get; set; }

    private RenderFragment? _lastChildContentRef;
    private bool _childContentChanged;

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (TabIndex.HasValue)
            attrs["tabindex"] = TabIndex.Value;

        if (Role is not null)
            attrs["role"] = Role;

        if (AriaLabel is not null)
            attrs["aria-label"] = AriaLabel;

        if (AriaDescribedBy is not null)
            attrs["aria-describedby"] = AriaDescribedBy;
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Tag);
        hc.Add(TabIndex);
        hc.Add(Role);
        hc.Add(AriaLabel);
        hc.Add(AriaDescribedBy);
    }

    protected override void OnParametersSet()
    {
        var cc = ChildContent;
        _childContentChanged = !ReferenceEquals(cc, _lastChildContentRef);
        _lastChildContentRef = cc;

        base.OnParametersSet();
    }

    protected override bool ShouldRender()
    {
        if (_childContentChanged)
        {
            _childContentChanged = false;
            return true;
        }

        return base.ShouldRender();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_childContentChanged)
        {
            _lastChildContentRef = ChildContent;
            _childContentChanged = false;
        }

        await base.OnAfterRenderAsync(firstRender);
    }
}