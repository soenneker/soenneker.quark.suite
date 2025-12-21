using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="ICheckInterop"/>
public sealed class CheckInterop : ICheckInterop
{
    private readonly AsyncInitializer _initializer;

    public CheckInterop(IResourceLoader resourceLoader)
    {
        var resourceLoader1 = resourceLoader;

        _initializer = new AsyncInitializer(async token =>
        {
            await resourceLoader1.LoadStyle("_content/Soenneker.Quark.Suite/css/check.css", cancellationToken: token);
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}
