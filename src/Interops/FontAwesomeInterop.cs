using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="IFontAwesomeInterop"/>
public sealed class FontAwesomeInterop : IFontAwesomeInterop
{
    private readonly AsyncInitializer _initializer;

    private const string _fontAwesomeCssUrl = "https://cdn.jsdelivr.net/npm/@fortawesome/fontawesome-free@7.1.0/css/all.min.css";
    private const string _fontAwesomeCssIntegrity = "sha256-4rTIfo5GQTi/7UJqoyUJQKzxW8VN/YBH31+Cy+vTZj4=";

    /// <summary>
    /// Initializes a new instance of the FontAwesomeInterop class.
    /// </summary>
    /// <param name="resourceLoader">The resource loader used to load Font Awesome CSS.</param>
    public FontAwesomeInterop(IResourceLoader resourceLoader)
    {
        _initializer = new AsyncInitializer(async token =>
        {
            await resourceLoader.LoadStyle(_fontAwesomeCssUrl, _fontAwesomeCssIntegrity, cancellationToken: token);
        });
    }

    /// <summary>
    /// Initializes Font Awesome CSS resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _initializer.Init(cancellationToken);
    }

    /// <summary>
    /// Disposes of the Font Awesome interop resources.
    /// </summary>
    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}
