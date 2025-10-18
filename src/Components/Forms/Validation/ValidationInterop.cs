using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

/// <inheritdoc cref="IValidationInterop"/>
public sealed class ValidationInterop : IValidationInterop
{
    private readonly AsyncSingleton _initializer;

    public ValidationInterop(IResourceLoader resourceLoader)
    {
        var loader = resourceLoader;

        _initializer = new AsyncSingleton(async (token, _) =>
        {
            await loader.LoadStyle("_content/Soenneker.Quark.Suite/css/validation.css", cancellationToken: token);
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}



