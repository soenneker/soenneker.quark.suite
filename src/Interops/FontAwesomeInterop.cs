using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="IFontAwesomeInterop"/>
public sealed class FontAwesomeInterop : IFontAwesomeInterop
{
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _fontAwesomeCssUrl = "https://cdn.jsdelivr.net/npm/@fortawesome/fontawesome-free@7.1.0/css/all.min.css";
    private const string _fontAwesomeCssIntegrity = "sha256-4rTIfo5GQTi/7UJqoyUJQKzxW8VN/YBH31+Cy+vTZj4=";

    private readonly IResourceLoader _resourceLoader;

    public FontAwesomeInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeCss);
    }

    private ValueTask InitializeCss(CancellationToken token)
    {
        return _resourceLoader.LoadStyle(_fontAwesomeCssUrl, _fontAwesomeCssIntegrity, cancellationToken: token);
    }

    /// <summary>
    /// Initializes Font Awesome CSS resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _initializer.Init(linked);
    }

    /// <summary>
    /// Disposes of the Font Awesome interop resources.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
