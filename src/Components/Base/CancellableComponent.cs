using System.Threading;
using System.Threading.Tasks;
using Soenneker.Utils.AtomicResources;

namespace Soenneker.Quark;

///<inheritdoc cref="ICancellableComponent"/>
public abstract class CancellableComponent : Component, ICancellableComponent
{
    public CancellationToken CancellationToken =>
        Disposed.IsTrue || AsyncDisposed.IsTrue ? CancellationToken.None : _cancellationTokenSource.TryGet()?.Token ?? CancellationToken.None;

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

    public Task Cancel()
    {
        var cts = _cancellationTokenSource.TryGet();
        return cts is null ? Task.CompletedTask : cts.CancelAsync();
    }

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