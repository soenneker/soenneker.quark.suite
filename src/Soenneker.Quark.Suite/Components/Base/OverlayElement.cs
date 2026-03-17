using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for overlay visibility state and overlay lifecycle callbacks.
/// </summary>
public abstract class OverlayElement : InteractiveElement
{
    [Parameter]
    public bool Visible { get; set; }

    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    [Parameter]
    public bool ShowBackdrop { get; set; } = true;

    [Parameter]
    public bool CloseOnBackdropClick { get; set; } = true;

    [Parameter]
    public bool CloseOnEscape { get; set; } = true;

    [Parameter]
    public EventCallback OnShow { get; set; }

    [Parameter]
    public EventCallback OnHide { get; set; }

    private bool _lastVisible;

    protected override bool ShouldRender()
    {
        if (Visible != _lastVisible)
        {
            _lastVisible = Visible;
            return true;
        }

        return base.ShouldRender();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        _lastVisible = Visible;
        return base.OnAfterRenderAsync(firstRender);
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Visible);
        hc.Add(ShowBackdrop);
        hc.Add(CloseOnBackdropClick);
        hc.Add(CloseOnEscape);
        hc.Add(VisibleChanged.HasDelegate);
        hc.Add(OnShow.HasDelegate);
        hc.Add(OnHide.HasDelegate);
    }
}
