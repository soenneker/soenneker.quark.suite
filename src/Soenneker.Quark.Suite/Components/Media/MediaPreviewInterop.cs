using System.Threading.Tasks;
using Soenneker.Blazor.Extensions.IJSObjectReferences;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;

namespace Soenneker.Quark;

public sealed class MediaPreviewInterop(IModuleImportUtil modules, QuarkOptions? options = null) : IMediaPreviewInterop
{
    private readonly string _path = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/mediapreviewinterop.js", options);

    public async ValueTask<string> Create(byte[] bytes, string contentType)
    {
        var module = await modules.GetContentModuleReference(_path);
        return await module.InvokeWithContextAsync<string>("create", bytes, contentType);
    }

    public async ValueTask Release(string url)
    {
        var module = await modules.GetContentModuleReference(_path);
        await module.InvokeVoidWithContextAsync("release", url);
    }

    public async ValueTask DisposeAsync() => await modules.DisposeContentModule(_path);
}
