using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;

namespace Soenneker.Quark;

public sealed class ValidationInterop : IValidationInterop
{
    public ValidationInterop(IResourceLoader resourceLoader)
    {
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
