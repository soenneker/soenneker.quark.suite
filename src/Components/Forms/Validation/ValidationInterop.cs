using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IValidationInterop"/>
public sealed class ValidationInterop : IValidationInterop
{
    private readonly AsyncInitializer _initializer;

    public ValidationInterop(IResourceLoader resourceLoader)
    {
        var loader = resourceLoader;

        _initializer = new AsyncInitializer(async token =>
        {
            await loader.LoadStyle("_content/Soenneker.Quark.Suite/css/validation.css", cancellationToken: token);
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}



