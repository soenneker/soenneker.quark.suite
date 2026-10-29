using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Extensions.IJSObjectReferences;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="IColorPickerInterop"/>
public sealed class ColorPickerInterop : IColorPickerInterop
{
    private readonly string _modulePath;

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    public ColorPickerInterop(IModuleImportUtil moduleImportUtil, QuarkOptions? quarkOptions = null)
    {
        _modulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/colorpickerinterop.js", quarkOptions);
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask<string?> PickColor(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeWithContextAsync<string?>("pickColor", linked);
        }
    }

    public async ValueTask<bool> RegisterCanvas(ElementReference root, DotNetObjectReference<ColorPicker> callbackReference, bool disabled,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeWithContextAsync<bool>("registerCanvas", linked, root, callbackReference, disabled);
        }
    }

    public async ValueTask UnregisterCanvas(ElementReference root, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("unregisterCanvas", linked, root);
        }
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
