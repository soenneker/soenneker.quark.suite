using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Bridges <c>popoverinterop.js</c> outside-interaction dismissal to select close (Radix-style pointer/focus-outside).
/// </summary>
public sealed class SelectOutsideCloseProxy
{
    private readonly Func<Task> close;

    /// <summary>
    /// Creates a proxy that invokes <paramref name="close"/> when an outside interaction should dismiss the select.
    /// </summary>
    public SelectOutsideCloseProxy(Func<Task> close)
    {
        this.close = close;
    }

    [JSInvokable]
    public Task Close()
    {
        return close();
    }
}
