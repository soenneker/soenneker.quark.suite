using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

internal sealed class HoverCardContext
{
    private readonly Func<bool> _getIsOpen;
    private readonly Func<Task> _open;
    private readonly Func<Task> _openImmediate;
    private readonly Func<Task> _close;
    private readonly Func<Task> _closeImmediate;
    private ElementReference _triggerElement;
    private bool _hasTriggerElement;

    public event Action? StateChanged;

    public HoverCardContext(Func<bool> getIsOpen, Func<Task> open, Func<Task> openImmediate, Func<Task> close, Func<Task> closeImmediate)
    {
        _getIsOpen = getIsOpen;
        _open = open;
        _openImmediate = openImmediate;
        _close = close;
        _closeImmediate = closeImmediate;

        var id = Guid.NewGuid().ToString("N");
        TriggerId = $"hover-card-trigger-{id}";
        ContentId = $"hover-card-content-{id}";
    }
    public string TriggerId { get; }

    public string ContentId { get; }

    public bool IsOpen => _getIsOpen();

    public bool HasTriggerElement => _hasTriggerElement;

    public ElementReference TriggerElement => _triggerElement;

    public void SetTriggerElement(ElementReference elementReference)
    {
        _triggerElement = elementReference;
        _hasTriggerElement = true;
        StateChanged?.Invoke();
    }

    public Task Open()
    {
        return _open();
    }

    /// <summary>
    /// Opens immediately (no open delay), e.g. for focus — matches Tooltip/Radix focus behavior.
    /// </summary>
    public Task OpenImmediate()
    {
        return _openImmediate();
    }

    public Task Close()
    {
        return _close();
    }

    /// <summary>
    /// Closes immediately (no close delay), e.g. for Escape key — matches Radix dismiss behavior inside nested layers.
    /// </summary>
    public Task CloseImmediate()
    {
        return _closeImmediate();
    }
}
