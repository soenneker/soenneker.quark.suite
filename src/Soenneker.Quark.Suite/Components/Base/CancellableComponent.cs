using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Atomics.Resources;

namespace Soenneker.Quark;

public abstract class CancellableComponent : Component, ICancellableComponent
{
    public CancellationToken CancellationToken =>
        IsDisposed ? CancellationToken.None
            : _cancellationTokenSource.GetOrCreate()
                                      ?.Token ?? CancellationToken.None;

    private readonly AtomicResource<CancellationTokenSource> _cancellationTokenSource;

    protected CancellableComponent() : this(CancellationToken.None)
    {
    }

    protected CancellableComponent(CancellationToken linkedToken)
    {
        _cancellationTokenSource = new AtomicResource<CancellationTokenSource>(
            factory: linkedToken.CanBeCanceled ? CreateLinkedFactory(linkedToken) : static () => new CancellationTokenSource(),
            teardown: static async cts =>
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

    private static Func<CancellationTokenSource> CreateLinkedFactory(CancellationToken linkedToken) =>
        () => CancellationTokenSource.CreateLinkedTokenSource(linkedToken);

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

}
