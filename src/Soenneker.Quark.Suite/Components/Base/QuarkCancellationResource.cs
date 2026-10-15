using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Atomics.ValueResources;

namespace Soenneker.Quark;

internal struct QuarkCancellationResource
{
    private static readonly Func<CancellationToken, CancellationTokenSource> _createSource = static linkedToken =>
        linkedToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(linkedToken) : new CancellationTokenSource();

    private static readonly Func<CancellationTokenSource, ValueTask> _disposeSource = static async source =>
    {
        try
        {
            await source.CancelAsync();
        }
        finally
        {
            source.Dispose();
        }
    };

    private readonly CancellationToken _linkedToken;

    private ValueAtomicResource<CancellationTokenSource> _resource;

    internal QuarkCancellationResource(CancellationToken linkedToken)
    {
        _linkedToken = linkedToken;
        _resource = default;
    }

    internal CancellationToken Token
    {
        get
        {
            try
            {
                return _resource.GetOrCreate(_linkedToken, _createSource, _disposeSource)?.Token ?? CancellationToken.None;
            }
            catch (ObjectDisposedException)
            {
                // Reset or disposal may detach the source after GetOrCreate.
                return CancellationToken.None;
            }
        }
    }

    internal Task Cancel()
    {
        CancellationTokenSource? source = _resource.TryGet();
        return source is null ? Task.CompletedTask : source.CancelAsync();
    }

    internal ValueTask ResetCancellation() => _resource.Reset(_linkedToken, _createSource, _disposeSource);

    internal ValueTask DisposeAsync() => _resource.DisposeAsync(_disposeSource);
}
