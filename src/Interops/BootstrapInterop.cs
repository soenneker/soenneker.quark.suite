using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="IBootstrapInterop"/>
public sealed class BootstrapInterop : IBootstrapInterop
{
    private readonly QuarkOptions _quarkOptions;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string CdnBaseUrl = "https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist";
    private const string CdnCssPath = "/css/bootstrap.min.css";
    private const string CdnJsPath = "/js/bootstrap.bundle.min.js";
    
    private const string LocalCssPath = "/css/bootstrap/bootstrap.min.css";
    private const string LocalJsPath = "/js/bootstrap/bootstrap.bundle.min.js";
    
    private const string CssIntegrity = "sha256-2FMn2Zx6PuH5tdBQDRNwrOo60ts5wWPC9R8jK67b3t4=";
    private const string JsIntegrity = "sha256-5P1JGBOIxI7FBAvT/mb1fCnI5n/NhQKzNUuW7Hq0fMc=";

    private readonly IResourceLoader _resourceLoader;

    public BootstrapInterop(IResourceLoader resourceLoader, QuarkOptions quarkOptions)
    {
        _resourceLoader = resourceLoader;
        _quarkOptions = quarkOptions;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private async ValueTask InitializeResources(CancellationToken token)
    {
        string cssUrl;
        string jsUrl;
        bool useIntegrity;

        if (_quarkOptions.BootstrapUseCdn)
        {
            cssUrl = $"{CdnBaseUrl}{CdnCssPath}";
            jsUrl = $"{CdnBaseUrl}{CdnJsPath}";
            useIntegrity = true;
        }
        else
        {
            cssUrl = LocalCssPath;
            jsUrl = LocalJsPath;
            useIntegrity = false;
        }

        if (useIntegrity)
        {
            await _resourceLoader.LoadStyle(cssUrl, CssIntegrity, cancellationToken: token);
            await _resourceLoader.LoadScript(jsUrl, JsIntegrity, cancellationToken: token);
        }
        else
        {
            await _resourceLoader.LoadStyle(cssUrl, cancellationToken: token);
            await _resourceLoader.LoadScript(jsUrl, cancellationToken: token);
        }
    }

    /// <summary>
    /// Initializes Bootstrap CSS and JavaScript resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _initializer.Init(linked);
    }

    /// <summary>
    /// Disposes of the Bootstrap interop resources.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}