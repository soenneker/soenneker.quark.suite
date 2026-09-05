using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ModuleImport.Dtos;

namespace Soenneker.Quark.Suite.Benchmarks;

internal sealed class BenchmarkModuleImportUtil : IModuleImportUtil
{
    private const string BradixModulePath = "./_content/Soenneker.Bradix.Suite/js/bradix.js";
    private readonly IJSRuntime _jsRuntime;

    public BenchmarkModuleImportUtil(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public ValueTask<ModuleImportItem> GetContentModule(string path, CancellationToken cancellationToken = default) =>
        throw new NotSupportedException("Bradix tests use module references directly.");

    public ValueTask<ModuleImportItem> GetExternalModule(string url, CancellationToken cancellationToken = default) =>
        throw new NotSupportedException("Bradix tests use module references directly.");

    public ValueTask<IJSObjectReference> GetContentModuleReference(string path, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken, BradixModulePath);
    }

    public ValueTask<IJSObjectReference> GetExternalModuleReference(string url, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken, BradixModulePath);
    }

    public ValueTask<bool> DisposeContentModule(string name)
    {
        return ValueTask.FromResult(true);
    }

    public ValueTask<bool> DisposeExternalModule(string url)
    {
        return ValueTask.FromResult(true);
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}
