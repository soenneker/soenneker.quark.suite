using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

///<inheritdoc cref="IBootstrapInterop"/>
public sealed class BootstrapInterop : IBootstrapInterop
{
    private readonly AsyncSingleton _initializer;

    private const string _bootstrapCssUrl = "https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css";
    private const string _bootstrapJsUrl = "https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js";
    private const string _bootstrapCssIntegrity = "sha256-2FMn2Zx6PuH5tdBQDRNwrOo60ts5wWPC9R8jK67b3t4=";
    private const string _bootstrapJsIntegrity = "sha256-5P1JGBOIxI7FBAvT/mb1fCnI5n/NhQKzNUuW7Hq0fMc=";

    public BootstrapInterop(IResourceLoader resourceLoader)
    {
        _initializer = new AsyncSingleton(async (token, arg) =>
        {
            // Load Bootstrap CSS using ResourceLoader
            await resourceLoader.LoadStyle(_bootstrapCssUrl, _bootstrapCssIntegrity, cancellationToken: token);

            // Load Bootstrap JavaScript using ResourceLoader
            await resourceLoader.LoadScript(_bootstrapJsUrl, _bootstrapJsIntegrity, cancellationToken: token);

            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _initializer.Init(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}