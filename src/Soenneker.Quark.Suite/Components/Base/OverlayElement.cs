using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.Ids;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for overlay visibility state and overlay lifecycle callbacks.
/// </summary>
public abstract class OverlayElement : InteractiveElement
{
    [Inject]
    protected IOverlayInterop OverlayInterop { get; set; } = null!;

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

    [Parameter]
    public bool TrapFocus { get; set; } = true;

    [Parameter]
    public bool LockScroll { get; set; } = true;

    [Parameter]
    public string? InitialFocusSelector { get; set; }

    private readonly string _overlayId = BlazorIdGenerator.New("quark-overlay");
    private bool _renderedVisible;
    private ElementReference _restoreFocusTarget;
    private bool _hasRestoreFocusTarget;
    private ElementReference _overlayContentElement;
    private bool _hasOverlayContentElement;
    private bool _overlayBehaviorActive;

    protected override bool ShouldRender()
    {
        if (Visible != _renderedVisible)
            return true;

        return base.ShouldRender();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        var wasVisible = _renderedVisible;
        _renderedVisible = Visible;

        return HandleAfterRender(firstRender, wasVisible);
    }

    private async Task HandleAfterRender(bool firstRender, bool wasVisible)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (Visible && !wasVisible)
        {
            await OnOverlayShown();
            return;
        }

        if (!Visible && wasVisible)
            await OnOverlayHidden();
    }

    internal void SetRestoreFocusTarget(ElementReference elementReference)
    {
        _restoreFocusTarget = elementReference;
        _hasRestoreFocusTarget = true;
    }

    internal void SetOverlayContentElement(ElementReference elementReference)
    {
        _overlayContentElement = elementReference;
        _hasOverlayContentElement = true;
    }

    protected virtual async Task OnOverlayShown()
    {
        if (!_hasOverlayContentElement)
            return;

        try
        {
            await OverlayInterop.Activate(_overlayId, _overlayContentElement, TrapFocus, LockScroll, InitialFocusSelector);
            _overlayBehaviorActive = true;
        }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException or ObjectDisposedException)
        {
        }
    }

    protected virtual async Task OnOverlayHidden()
    {
        await DeactivateOverlayBehavior();
        await RestoreFocus();
    }

    protected async Task RestoreFocus()
    {
        if (!_hasRestoreFocusTarget)
            return;

        try
        {
            await _restoreFocusTarget.FocusAsync();
        }
        catch
        {
        }
    }

    private async Task DeactivateOverlayBehavior()
    {
        if (!_overlayBehaviorActive)
            return;

        try
        {
            await OverlayInterop.Deactivate(_overlayId, LockScroll);
        }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException or ObjectDisposedException)
        {
        }
        finally
        {
            _overlayBehaviorActive = false;
        }
    }

    public override async ValueTask DisposeAsync()
    {
        await DeactivateOverlayBehavior();
        await base.DisposeAsync();
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
        hc.Add(TrapFocus);
        hc.Add(LockScroll);
        hc.Add(InitialFocusSelector);
    }
}
