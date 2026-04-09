using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for overlay visibility state and overlay lifecycle callbacks.
/// Modal Escape is handled in <see cref="IOverlayInterop"/> (document capture, stack-aware) for nested overlays.
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

    private readonly string _overlayId = $"overlay-{Guid.NewGuid():N}";
    private bool _renderedVisible;
    private ElementReference _restoreFocusTarget;
    private bool _hasRestoreFocusTarget;
    private ElementReference _overlayContentElement;
    private bool _hasOverlayContentElement;
    private bool _overlayBehaviorActive;
    private DotNetObjectReference<OverlayElement>? _escapeInvoker;

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

        return HandleAfterRenderAsync(firstRender, wasVisible);
    }

    private async Task HandleAfterRenderAsync(bool firstRender, bool wasVisible)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (Visible && !wasVisible)
        {
            await OnOverlayShownAsync();
            return;
        }

        if (!Visible && wasVisible)
            await OnOverlayHiddenAsync();
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

    protected virtual async Task OnOverlayShownAsync()
    {
        if (!_hasOverlayContentElement)
            return;

        try
        {
            _escapeInvoker?.Dispose();
            _escapeInvoker = DotNetObjectReference.Create(this);
            await OverlayInterop.Activate(_overlayId, _overlayContentElement, TrapFocus, LockScroll, InitialFocusSelector, _escapeInvoker);
            _overlayBehaviorActive = true;
        }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException or ObjectDisposedException)
        {
            _escapeInvoker?.Dispose();
            _escapeInvoker = null;
        }
    }

    /// <summary>
    /// Invoked from the overlay module when Escape is pressed and this overlay is the top of the stack.
    /// Returns whether the key event should be stopped so lower layers do not also react.
    /// </summary>
    [JSInvokable]
    public async Task<bool> OnEscapeKeyAsync()
    {
        if (!CloseOnEscape || !ShouldDismissFromEscape())
            return false;

        await DismissFromEscapeAsync();
        return true;
    }

    /// <summary>
    /// When <c>false</c>, Escape is not turned into a dismiss (event may propagate — e.g. <c>Static</c> dialogs).
    /// </summary>
    protected virtual bool ShouldDismissFromEscape()
    {
        return true;
    }

    /// <summary>
    /// Override to dismiss when the document-level Escape handler fires for this overlay (top of stack).
    /// </summary>
    protected virtual Task DismissFromEscapeAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnOverlayHiddenAsync()
    {
        await DeactivateOverlayBehaviorAsync();
        await RestoreFocusAsync();
    }

    protected async Task RestoreFocusAsync()
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

    private async Task DeactivateOverlayBehaviorAsync()
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
            _escapeInvoker?.Dispose();
            _escapeInvoker = null;
        }
    }

    public override async ValueTask DisposeAsync()
    {
        await DeactivateOverlayBehaviorAsync();
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
