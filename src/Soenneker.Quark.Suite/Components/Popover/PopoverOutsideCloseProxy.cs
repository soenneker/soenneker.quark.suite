using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public sealed class PopoverOutsideCloseProxy
{
    private readonly Func<Task> _close;

    internal PopoverOutsideCloseProxy(Func<Task> close)
    {
        _close = close;
    }

    [JSInvokable]
    public Task Close()
    {
        return _close();
    }
}
