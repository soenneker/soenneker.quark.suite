using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;

namespace Soenneker.Quark;

public sealed class SpinnerInterop(IResourceLoader resourceLoader) : ISpinnerInterop
{
    private const string StylePath = "_content/Soenneker.Quark.Suite/css/spinner.css";

    public ValueTask Initialize(CancellationToken cancellationToken = default) =>
        resourceLoader.LoadStyle(StylePath, cancellationToken: cancellationToken);

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
