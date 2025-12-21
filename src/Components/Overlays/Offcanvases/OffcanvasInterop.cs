using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="IOffcanvasInterop"/>
public sealed class OffcanvasInterop : IOffcanvasInterop
{
    private readonly AsyncInitializer _initializer;

    public OffcanvasInterop(IResourceLoader resourceLoader)
    {
        var resourceLoader1 = resourceLoader;

        _initializer = new AsyncInitializer(async token =>
        {
            await resourceLoader1.LoadStyle("_content/Soenneker.Quark.Suite/css/offcanvas.css", cancellationToken: token);
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}
