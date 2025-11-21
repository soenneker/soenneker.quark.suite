using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

/// <inheritdoc cref="ITablesInterop"/>
public sealed class TablesInterop : ITablesInterop
{
    private readonly AsyncSingleton _styleInitializer;

    public TablesInterop(IResourceLoader resourceLoader)
    {
        _styleInitializer = new AsyncSingleton(async (token, _) =>
        {
            await resourceLoader.LoadStyle("_content/Soenneker.Quark.Suite/css/table.css", cancellationToken: token);

            return new object();
        });
    }

    /// <summary>
    /// Initializes table functionality by loading required CSS resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the initialization operation.</returns>
    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _styleInitializer.Init(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _styleInitializer.DisposeAsync();
    }
}
