using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark;

/// <summary>Retains child render frames until their explicit state changes.</summary>
/// <remarks>The version must include every input read by the fragment, including captured state.</remarks>
public sealed class ContentRenderBoundary<TVersion> : ComponentBase
{
    private TVersion _rendered = default!;
    private bool _initialized;
    private bool _shouldRender;

    /// <summary>Gets or sets the complete state of the retained content.</summary>
    [Parameter] public TVersion Version { get; set; } = default!;
    /// <summary>Gets or sets whether to render without comparing versions.</summary>
    [Parameter] public bool AlwaysRender { get; set; }
    /// <summary>Gets or sets the retained content.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void OnParametersSet()
    {
        _shouldRender = AlwaysRender || !_initialized || !EqualityComparer<TVersion>.Default.Equals(_rendered, Version);
        _rendered = Version;
        _initialized = true;
    }

    protected override bool ShouldRender() => _shouldRender;
    protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, ChildContent);
}
