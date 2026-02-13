using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IScoreInterop"/>
public sealed class ScoreInterop : IScoreInterop
{
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();
    private readonly IResourceLoader _resourceLoader;

    private const string _cssPath = "_content/Soenneker.Quark.Suite/css/score.css";

    public ScoreInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeCss);
    }

    private ValueTask InitializeCss(CancellationToken token)
    {
        return _resourceLoader.LoadStyle(_cssPath, cancellationToken: token);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _initializer.Init(linked);
    }

    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
