using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public abstract class CancellableComponent : Component, ICancellableComponent
{
    private QuarkCancellationResource _cancellation;

    public CancellationToken CancellationToken => IsDisposed ? CancellationToken.None : _cancellation.Token;

    protected CancellableComponent() : this(CancellationToken.None)
    {
    }

    protected CancellableComponent(CancellationToken linkedToken)
    {
        _cancellation = new QuarkCancellationResource(linkedToken);
    }

    public Task Cancel() => _cancellation.Cancel();

    public ValueTask ResetCancellation() => _cancellation.ResetCancellation();

    public override ValueTask DisposeAsync()
    {
        ValueTask cancellation = _cancellation.DisposeAsync();
        if (!cancellation.IsCompletedSuccessfully)
            return CompleteDisposalAsync(cancellation);

        cancellation.GetAwaiter().GetResult();
        return base.DisposeAsync();
    }

    private async ValueTask CompleteDisposalAsync(ValueTask cancellation)
    {
        await cancellation;
        await base.DisposeAsync();
    }
}
