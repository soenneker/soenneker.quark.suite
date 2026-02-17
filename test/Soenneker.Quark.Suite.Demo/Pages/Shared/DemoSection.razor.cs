using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Demo.Pages.Shared;

public partial class DemoSection
{
    [Parameter] public string? Title { get; set; }
    [Parameter] public string? Description { get; set; }
    [Parameter] public string? Code { get; set; }
    [Parameter] public string? CodeLanguage { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
