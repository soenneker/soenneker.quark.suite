using Microsoft.AspNetCore.Components;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Atomics.Resources;

namespace Soenneker.Quark;

/// <inheritdoc cref="ICancellableLayout"/>
public abstract class CancellableLayout : LayoutComponentBase, ICancellableLayout
{
    private readonly AtomicResource<CancellationTokenSource> _atomic;

    protected CancellableLayout() : this(CancellationToken.None)
    {
    }

    protected CancellableLayout(CancellationToken linkedToken)
    {
        _atomic = new AtomicResource<CancellationTokenSource>(
            factory: linkedToken.CanBeCanceled ? CreateLinkedFactory(linkedToken) : static () => new CancellationTokenSource(),
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

    private static System.Func<CancellationTokenSource> CreateLinkedFactory(CancellationToken token) =>
        () => CancellationTokenSource.CreateLinkedTokenSource(token);

    public CancellationToken CancellationToken => _atomic.GetOrCreate()?.Token ?? CancellationToken.None;

    public Task Cancel()
    {
        var cts = _atomic.TryGet();
        return cts is null ? Task.CompletedTask : cts.CancelAsync();
    }

    public ValueTask ResetCancellation() => _atomic.Reset();

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public virtual ValueTask DisposeAsync() => _atomic.DisposeAsync();
}
