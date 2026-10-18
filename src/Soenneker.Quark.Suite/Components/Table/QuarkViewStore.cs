using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
namespace Soenneker.Quark;
public sealed class QuarkViewStore(IModuleImportUtil modules) : IQuarkViewStore
{
    public async ValueTask<IReadOnlyList<QuarkSavedView>> Load(string key, CancellationToken cancellationToken = default)
    {
        var module = await modules.GetContentModuleReference("./_content/Soenneker.Quark.Suite/js/workflowinterop.js", cancellationToken);
        var json = await module.InvokeAsync<string?>("loadViews", cancellationToken, key);
        return json is null ? [] : QuarkViewValidation.Validate(JsonSerializer.Deserialize(json, QuarkViewJsonContext.Default.ListQuarkSavedView) ?? throw new JsonException("Invalid saved views."));
    }
    public async ValueTask Save(string key, IReadOnlyList<QuarkSavedView> views, CancellationToken cancellationToken = default)
    {
        var module = await modules.GetContentModuleReference("./_content/Soenneker.Quark.Suite/js/workflowinterop.js", cancellationToken);
        await module.InvokeVoidAsync("saveViews", cancellationToken, key, JsonSerializer.Serialize(QuarkViewValidation.Validate(views).ToList(), QuarkViewJsonContext.Default.ListQuarkSavedView));
    }
}
