using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISpinnerInterop" />
public sealed class SpinnerInterop : ISpinnerInterop
{
    private const string _stylePath = "_content/Soenneker.Quark.Suite/css/spinner.css";

    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();
    private readonly IResourceLoader _resourceLoader;

    public SpinnerInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeCss);
    }

    private ValueTask InitializeCss(CancellationToken token) => _resourceLoader.LoadStyle(_stylePath, cancellationToken: token);

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _initializer.Init(linked);
    }

    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
