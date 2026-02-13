using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="ICancellableElement"/>
public abstract class CancellableElement : CancellableComponent, ICancellableElement
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private RenderFragment? _lastChildContentRef;
    private bool _childContentChanged;

    protected override void OnParametersSet()
    {
        RenderFragment? cc = ChildContent;
        _childContentChanged = !ReferenceEquals(cc, _lastChildContentRef);
        _lastChildContentRef = cc;

        base.OnParametersSet();
    }

    protected override bool ShouldRender()
    {
        if (QuarkOptions.AlwaysRender)
            return true;

        if (_childContentChanged)
        {
            // Consume the change so subsequent render-gate checks don’t keep firing
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