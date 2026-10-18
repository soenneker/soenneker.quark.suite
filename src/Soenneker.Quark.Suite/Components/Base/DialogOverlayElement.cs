using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;

public abstract class DialogOverlayElement : OverlayElement, IDialogOverlay
{
    private protected readonly OverlayLabelContext _context;
    protected DialogOverlayElement(string scope)
    {
        _context = new(scope);
        LockScroll = false;
    }

    [Parameter] public bool Static { get; set; }
    [Parameter] public EventCallback OnBackdropClick { get; set; }
    internal bool DismissOnOutside => !Static && CloseOnBackdropClick;
    internal bool DismissOnEscape => !Static && CloseOnEscape;
    internal Task NotifyBackdropClick() => OnBackdropClick.InvokeAsync();
    public Task Show() => SetVisible(true);
    public Task Hide() => SetVisible(false);
    protected Task HandleOpenChanged(bool open) => SetVisible(open);

    private async Task SetVisible(bool visible)
    {
        if (Visible == visible) return;
        Visible = visible;
        await VisibleChanged.InvokeAsync(visible);
        if (visible) await OnShow.InvokeAsync();
        else await OnHide.InvokeAsync();
        await RefreshOffThread();
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        hc.Add(Visible);
        hc.Add(ShowBackdrop);
        hc.Add(Static);
        hc.Add(CloseOnBackdropClick);
        hc.Add(CloseOnEscape);
        hc.Add(_context.ContentId);
        hc.Add(_context.HasTitle);
        hc.Add(_context.HasDescription);
        hc.Add(_context.TitleId);
        hc.Add(_context.DescriptionId);
    }
}
