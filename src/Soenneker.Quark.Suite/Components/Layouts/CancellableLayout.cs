using Microsoft.AspNetCore.Components;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public abstract class CancellableLayout : LayoutComponentBase, ICancellableLayout
{
    private QuarkCancellationResource _cancellation;

    protected CancellableLayout() : this(CancellationToken.None)
    {
    }

    protected CancellableLayout(CancellationToken linkedToken)
    {
        _cancellation = new QuarkCancellationResource(linkedToken);
    }

    public CancellationToken CancellationToken => _cancellation.Token;

    public Task Cancel() => _cancellation.Cancel();

    public ValueTask ResetCancellation() => _cancellation.ResetCancellation();

    public virtual ValueTask DisposeAsync() => _cancellation.DisposeAsync();
}
