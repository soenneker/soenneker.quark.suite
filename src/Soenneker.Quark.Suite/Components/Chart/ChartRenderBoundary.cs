using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark;

/// <summary>
/// Keeps expensive chart geometry out of interaction-only render passes.
/// </summary>
/// <remarks>
/// The parent increments <see cref="Version"/> whenever data, geometry options, or static SVG content changes.
/// Tooltip, cursor, and active-point updates can then render around this boundary without rebuilding the retained SVG frames.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ChartRenderBoundary : ComponentBase
{
    private int _renderedVersion;
    private bool _hasRenderedVersion;
    private bool _shouldRender;

    /// <summary>Gets or sets the generation of the static chart content.</summary>
    [Parameter]
    public int Version { get; set; }

    /// <summary>Gets or sets the static SVG content retained until <see cref="Version"/> changes.</summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void OnParametersSet()
    {
        _shouldRender = !_hasRenderedVersion || Version != _renderedVersion;

        if (_shouldRender)
        {
            _renderedVersion = Version;
            _hasRenderedVersion = true;
        }
    }

    protected override bool ShouldRender() => _shouldRender;

    protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, ChildContent);
}
