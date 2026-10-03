using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark.Suite.Tests;

public sealed class AttributeStringReuseProbe : RenderComponent
{
    private string? _class;
    private string? _style;
    private string? _prefix;

    public EventCallback<string> Handler { get; set; }

    public Dictionary<string, object> BuildEvents() => BuildAttributes();

    protected override void BuildOwnedAttributes(Dictionary<string, object> attributes)
    {
        if (Handler.HasDelegate)
            SetEventAttribute(attributes, "onchange", Handler);
    }

    public void Build(Dictionary<string, object> attributes, string classes, string style)
    {
        var cls = new PooledStringBuilder(stackalloc char[64]);
        var sty = new PooledStringBuilder(stackalloc char[64]);
        try
        {
            cls.Append(classes);
            sty.Append(style);
            BuildClassAndStyleAttributes(attributes, ref cls, ref sty, ref _class, ref _style);
        }
        finally
        {
            cls.Dispose();
            sty.Dispose();
        }
    }

    public void Prepend(Dictionary<string, object> attributes, string prefix) => PrependClassAttribute(attributes, prefix, ref _prefix);
}
