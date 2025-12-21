using System.Threading;
using System.Threading.Tasks;
using Soenneker.Atomics.Resources;

namespace Soenneker.Quark;

///<inheritdoc cref="ICancellableComponent"/>
public abstract class CancellableComponent : Component, ICancellableComponent
{
    /// <summary>
    /// Gets the current cancellation token for in-flight work.
    /// Returns <see cref="CancellationToken.None"/> after disposal.
    /// </summary>
    public CancellationToken CancellationToken =>
        Disposed.Value || AsyncDisposed.Value
            ? CancellationToken.None
            : _cancellationTokenSource.TryGet()
                                      ?.Token ?? CancellationToken.None;

    private readonly AtomicResource<CancellationTokenSource> _cancellationTokenSource;

    protected CancellableComponent() : this(CancellationToken.None)
    {
    }

    /// <summary>
    /// Optionally link to an external token so parent cancellation flows into this component.
    /// </summary>
    protected CancellableComponent(CancellationToken linkedToken)
    {
        _cancellationTokenSource = new AtomicResource<CancellationTokenSource>(
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

    /// <summary>
    /// Cancels any in-flight work. No-op if nothing has started.
    /// </summary>
    /// <returns>A task representing the cancellation operation.</returns>
    public Task Cancel()
    {
        var cts = _cancellationTokenSource.TryGet();
        return cts is null ? Task.CompletedTask : cts.CancelAsync();
    }

    /// <summary>
    /// Cancels current work and swaps in a fresh token/source for new work.
    /// </summary>
    /// <returns>A value task representing the reset operation.</returns>
    public ValueTask ResetCancellation() => _cancellationTokenSource.Reset();

    public override async ValueTask DisposeAsync()
    {
        await _cancellationTokenSource.DisposeAsync();

        await base.DisposeAsync();
    }

    public override void Dispose()
    {
        _cancellationTokenSource.Dispose();

        base.Dispose();
    }
}