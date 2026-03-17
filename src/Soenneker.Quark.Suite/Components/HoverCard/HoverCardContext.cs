using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class HoverCardContext
{
    private readonly Func<bool> _getIsOpen;
    private readonly Func<Task> _open;
    private readonly Func<Task> _close;

    public HoverCardContext(Func<bool> getIsOpen, Func<Task> open, Func<Task> close)
    {
        _getIsOpen = getIsOpen;
        _open = open;
        _close = close;

        var id = Guid.NewGuid().ToString("N");
        TriggerId = $"hover-card-trigger-{id}";
        ContentId = $"hover-card-content-{id}";
    }

    public string TriggerId { get; }

    public string ContentId { get; }

    public bool IsOpen => _getIsOpen();

    public Task Open()
    {
        return _open();
    }

    public Task Close()
    {
        return _close();
    }
}
