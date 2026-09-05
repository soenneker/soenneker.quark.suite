using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;

namespace Soenneker.Quark.Suite.Benchmarks;

internal sealed class BenchmarkResourceLoader : IResourceLoader
{
    public ValueTask LoadScript(string uri, string? integrity = null, string? crossOrigin = "anonymous", bool loadInHead = false, bool async = false,
        bool defer = false, CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask LoadModuleScript(string uri, string? integrity = null, string? crossOrigin = "anonymous", bool loadInHead = false,
        CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask LoadScriptAndWaitForVariable(string uri, string variableName, string? integrity = null, string? crossOrigin = "anonymous",
        bool loadInHead = false, bool async = false, bool defer = false, int delay = 16, int? timeout = null, CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask LoadModuleScriptAndWaitForVariable(string uri, string variableName, string? integrity = null, string? crossOrigin = "anonymous",
        bool loadInHead = false, int delay = 16, int? timeout = null, CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask LoadStyle(string uri, string? integrity = null, string? crossOrigin = "anonymous", string? media = "all", string? type = "text/css",
        CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask WaitForVariable(string variableName, int delay = 16, int? timeout = null, CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}
