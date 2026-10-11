using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;

namespace Soenneker.Quark;

public sealed class SpinnerInterop(IResourceLoader resourceLoader, QuarkOptions? quarkOptions = null) : ISpinnerInterop
{
    private readonly string StylePath = QuarkAssetPath.Css("_content/Soenneker.Quark.Suite/css/spinner.css", quarkOptions);

    public ValueTask Initialize(CancellationToken cancellationToken = default) =>
        resourceLoader.LoadStyle(StylePath, cancellationToken: cancellationToken);

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
