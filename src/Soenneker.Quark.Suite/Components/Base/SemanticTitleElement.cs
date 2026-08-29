using System;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISemanticTitleElement"/>
public abstract class SemanticTitleElement : TypographyElement, ISemanticTitleElement
{
    /// <summary>
    /// Gets or sets the semantic heading level. When omitted, the component retains its neutral default element.
    /// </summary>
    [Parameter]
    public HeadingLevel? HeadingLevel { get; set; }

    /// <summary>
    /// Gets the neutral element used when <see cref="HeadingLevel"/> is omitted.
    /// </summary>
    protected virtual string DefaultTitleTag => "div";

    /// <summary>
    /// Renders the title using the selected semantic element.
    /// </summary>
    protected RenderFragment RenderSemanticTitle() => builder =>
    {
        builder.OpenElement(0, ResolveTitleTag());
        builder.AddMultipleAttributes(1, BuildAttributes());
        builder.AddContent(2, ChildContent);
        builder.CloseElement();
    };

    private string ResolveTitleTag() => HeadingLevel switch
    {
        global::Soenneker.Quark.HeadingLevel.H1 => "h1",
        global::Soenneker.Quark.HeadingLevel.H2 => "h2",
        global::Soenneker.Quark.HeadingLevel.H3 => "h3",
        global::Soenneker.Quark.HeadingLevel.H4 => "h4",
        global::Soenneker.Quark.HeadingLevel.H5 => "h5",
        global::Soenneker.Quark.HeadingLevel.H6 => "h6",
        _ => DefaultTitleTag
    };

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        hc.Add(HeadingLevel);
    }
}
