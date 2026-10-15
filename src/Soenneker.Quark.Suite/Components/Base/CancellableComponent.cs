using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Atomics.Resources;

namespace Soenneker.Quark;

public abstract class CancellableComponent : Component, ICancellableComponent
{
    private static readonly object _disposed = new();

    private readonly CancellationToken _linkedToken;

    // Publish the lazy resource and terminal disposal state through one field.
    // Components that never start cancellable work need no resource or factory.
    private object? _cancellationTokenSource;

    public CancellationToken CancellationToken
    {
        get
        {
            if (IsDisposed)
                return CancellationToken.None;

            try
            {
                return GetOrCreateResource()?.GetOrCreate()?.Token ?? CancellationToken.None;
            }
            catch (ObjectDisposedException)
            {
                // Reset or disposal may detach the source after GetOrCreate.
                return CancellationToken.None;
            }
        }
    }

    protected CancellableComponent() : this(CancellationToken.None)
    {
    }

    protected CancellableComponent(CancellationToken linkedToken)
    {
        _linkedToken = linkedToken;
    }

    private AtomicResource<CancellationTokenSource>? GetOrCreateResource()
    {
        object? resource = Volatile.Read(ref _cancellationTokenSource);
        return resource is null ? CreateResource() : resource as AtomicResource<CancellationTokenSource>;
    }

    private AtomicResource<CancellationTokenSource>? CreateResource()
    {
        var created = new AtomicResource<CancellationTokenSource>(
            factory: _linkedToken.CanBeCanceled ? CreateLinkedFactory(_linkedToken) : static () => new CancellationTokenSource(),
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

        object? observed = Interlocked.CompareExchange(ref _cancellationTokenSource, created, null);
        return observed is null ? created : observed as AtomicResource<CancellationTokenSource>;
    }

    private static Func<CancellationTokenSource> CreateLinkedFactory(CancellationToken linkedToken) =>
        () => CancellationTokenSource.CreateLinkedTokenSource(linkedToken);

    public Task Cancel()
    {
        var cts = (Volatile.Read(ref _cancellationTokenSource) as AtomicResource<CancellationTokenSource>)?.TryGet();
        return cts is null ? Task.CompletedTask : cts.CancelAsync();
    }

    public ValueTask ResetCancellation() => GetOrCreateResource()?.Reset() ?? default;

    public override ValueTask DisposeAsync()
    {
        var resource = Interlocked.Exchange(ref _cancellationTokenSource, _disposed) as AtomicResource<CancellationTokenSource>;
        if (resource is not null)
        {
            ValueTask cancellation = resource.DisposeAsync();
            if (!cancellation.IsCompletedSuccessfully)
                return CompleteDisposalAsync(cancellation);

            cancellation.GetAwaiter().GetResult();
        }

        return base.DisposeAsync();
    }

    private async ValueTask CompleteDisposalAsync(ValueTask cancellation)
    {
        await cancellation;
        await base.DisposeAsync();
    }
}
