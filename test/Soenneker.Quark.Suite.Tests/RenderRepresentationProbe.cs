using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark.Suite.Tests;

public sealed class RenderRepresentationProbe : RenderComponent
{
    [Parameter] public object? Value { get; set; }
    protected override bool AlwaysRender => false;
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var rendered = Value ?? (Attributes is not null && Attributes.TryGetValue("data-value", out var attribute) ? attribute : null);
        builder.AddContent(0, rendered switch
        {
            DateTime date => date.ToString("O", CultureInfo.InvariantCulture),
            DateTimeOffset date => date.ToString("O", CultureInfo.InvariantCulture),
            IFormattable value => value.ToString(null, CultureInfo.InvariantCulture),
            _ => rendered?.ToString()
        });
    }
}
