using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IValidationInterop"/>
public sealed class ValidationInterop : IValidationInterop
{
    private readonly AsyncInitializer _initializer;

    private readonly IResourceLoader _resourceLoader;

    public ValidationInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeCss);
    }

    private ValueTask InitializeCss(CancellationToken token)
    {
        return _resourceLoader.LoadStyle("_content/Soenneker.Quark.Suite/css/validation.css", cancellationToken: token);
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}



