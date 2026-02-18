using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark.Suite.Demo.Pages.Shared;

public partial class DemoSection
{
    [Parameter] public string? Title { get; set; }
    [Parameter] public string? Description { get; set; }
    [Parameter] public string? Code { get; set; }
    [Parameter] public string? CodeLanguage { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    private bool _showCode;
    private string _copyButtonLabel = "Copy";

    private void ToggleCode(bool showCode)
    {
        _showCode = showCode;
    }

    private async Task CopyCodeAsync()
    {
        if (string.IsNullOrEmpty(Code)) return;
        try
        {
            await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Code);
            _copyButtonLabel = "Copied!";
            StateHasChanged();
            await Task.Delay(1500);
            _copyButtonLabel = "Copy";
            StateHasChanged();
        }
        catch
        {
            _copyButtonLabel = "Copy";
        }
    }
}
