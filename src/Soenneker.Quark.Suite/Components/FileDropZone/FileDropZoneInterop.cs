using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

public sealed class FileDropZoneInterop : IFileDropZoneInterop
{
    private const string ModulePath = "./_content/Soenneker.Quark.Suite/js/filedropzoneinterop.js";
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    public FileDropZoneInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Initialize(string owner, ElementReference element, DotNetObjectReference<FileDropZone> callbackReference,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(ModulePath, linked);
            await module.InvokeVoidAsync("initialize", linked, owner, element, callbackReference);
        }
    }

    public async ValueTask<string?> CreatePreview(string owner, string inputId, int index, string fileId, string contentType,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(ModulePath, linked);
            return await module.InvokeAsync<string?>("createPreview", linked, owner, inputId, index, fileId, contentType);
        }
    }

    public async ValueTask RetainPreviews(string owner, string[] fileIds, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(ModulePath, linked);
            await module.InvokeVoidAsync("retain", linked, owner, JsonSerializer.SerializeToElement(fileIds, QuarkInteropJsonContext.Default.StringArray));
        }
    }

    public async ValueTask Destroy(string owner, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(ModulePath, linked);
            await module.InvokeVoidAsync("dispose", linked, owner);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(ModulePath);
    }
}
