using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

///<inheritdoc cref="IBootstrapInterop"/>
public sealed class BootstrapInterop : IBootstrapInterop
{
    private readonly QuarkOptions _quarkOptions;
    private readonly AsyncSingleton _initializer;

    private const string CdnBaseUrl = "https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist";
    private const string CdnCssPath = "/css/bootstrap.min.css";
    private const string CdnJsPath = "/js/bootstrap.bundle.min.js";
    
    private const string LocalCssPath = "/css/bootstrap/bootstrap.min.css";
    private const string LocalJsPath = "/js/bootstrap/bootstrap.bundle.min.js";
    
    private const string CssIntegrity = "sha256-2FMn2Zx6PuH5tdBQDRNwrOo60ts5wWPC9R8jK67b3t4=";
    private const string JsIntegrity = "sha256-5P1JGBOIxI7FBAvT/mb1fCnI5n/NhQKzNUuW7Hq0fMc=";

    /// <summary>
    /// Initializes a new instance of the BootstrapInterop class.
    /// </summary>
    /// <param name="resourceLoader">The resource loader used to load Bootstrap CSS and JavaScript.</param>
    /// <param name="quarkOptions">The Quark configuration options.</param>
    public BootstrapInterop(IResourceLoader resourceLoader, QuarkOptions quarkOptions)
    {
        _quarkOptions = quarkOptions;

        _initializer = new AsyncSingleton(async (token, arg) =>
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
                await resourceLoader.LoadStyle(cssUrl, CssIntegrity, cancellationToken: token);
                await resourceLoader.LoadScript(jsUrl, JsIntegrity, cancellationToken: token);
            }
            else
            {
                await resourceLoader.LoadStyle(cssUrl, cancellationToken: token);
                await resourceLoader.LoadScript(jsUrl, cancellationToken: token);
            }

            return new object();
        });
    }

    /// <summary>
    /// Initializes Bootstrap CSS and JavaScript resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _initializer.Init(cancellationToken);
    }

    /// <summary>
    /// Disposes of the Bootstrap interop resources.
    /// </summary>
    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}