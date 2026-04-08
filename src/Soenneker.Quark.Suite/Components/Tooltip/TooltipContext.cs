using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class TooltipContext
{
    private readonly Func<bool> _getIsOpen;
    private readonly Func<Task> _open;
    private readonly Func<Task> _openImmediate;
    private readonly Func<Task> _close;

    public TooltipContext(Func<bool> getIsOpen, Func<Task> open, Func<Task> openImmediate, Func<Task> close)
    {
        _getIsOpen = getIsOpen;
        _open = open;
        _openImmediate = openImmediate;
        _close = close;

        var id = Guid.NewGuid().ToString("N");
        TriggerId = $"tooltip-trigger-{id}";
        ContentId = $"tooltip-content-{id}";
    }

    public string TriggerId { get; }

    public string ContentId { get; }

    public bool IsOpen => _getIsOpen();

    public Task Open()
    {
        return _open();
    }

    public Task OpenImmediate()
    {
        return _openImmediate();
    }

    public Task Close()
    {
        return _close();
    }
}
