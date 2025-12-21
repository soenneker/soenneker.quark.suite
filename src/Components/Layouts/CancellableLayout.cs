using Microsoft.AspNetCore.Components;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Atomics.Resources;

namespace Soenneker.Quark;

/// <summary>
/// A base layout that provides a per-component CancellationTokenSource, lazily
/// </summary>
public abstract class CancellableLayout : LayoutComponentBase, ICancellableLayout
{
    private readonly AtomicResource<CancellationTokenSource> _atomic;

    protected CancellableLayout() : this(CancellationToken.None)
    {
    }

    /// <summary>
    /// Optionally link to an external token so parent cancellation flows into this component.
    /// </summary>
    protected CancellableLayout(CancellationToken linkedToken)
    {
        _atomic = new AtomicResource<CancellationTokenSource>(
            factory: () => linkedToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(linkedToken) : new CancellationTokenSource(),
            teardown: async cts =>
            {
                try
                {
                    await cts.CancelAsync();
                }
                catch
                {
                    /* ignore */
                }

                cts.Dispose();
            });
    }

    public CancellationToken CancellationToken => _atomic.GetOrCreate()?.Token ?? CancellationToken.None;

    public Task Cancel()
    {
        var cts = _atomic.TryGet();
        return cts is null ? Task.CompletedTask : cts.CancelAsync();
    }

    public ValueTask ResetCancellation() => _atomic.Reset();

    public virtual ValueTask DisposeAsync() => _atomic.DisposeAsync();
}
