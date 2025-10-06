using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="IElement"/>
public abstract class Element : Component, IElement
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private RenderFragment? _lastChildContentRef;
    private bool _childContentChanged;

    protected override void OnParametersSet()
    {
        // Detect route/body swaps or any parent-provided fragment changes
        _childContentChanged = !ReferenceEquals(ChildContent, _lastChildContentRef);
        base.OnParametersSet();
    }

    protected override bool ShouldRender()
    {
        // Force a render when the fragment reference changes, otherwise defer to base render gate
        return _childContentChanged || base.ShouldRender();
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