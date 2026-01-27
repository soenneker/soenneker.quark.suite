using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="ISnackbarInterop"/>
public sealed class SnackbarInterop : ISnackbarInterop
{
    private readonly AsyncInitializer _cssInitializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _cssPath = "_content/Soenneker.Quark.Suite/css/snackbar.css";

    private readonly IResourceLoader _resourceLoader;

    public SnackbarInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _cssInitializer = new AsyncInitializer(InitializeCss);
    }

    private ValueTask InitializeCss(CancellationToken token)
    {
        return _resourceLoader.LoadStyle(_cssPath, cancellationToken: token);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _cssInitializer.Init(linked);
    }

    public async ValueTask DisposeAsync()
    {
        await _cssInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
