using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark;

// Parsed once per shared provider/generated SVG string. Attribute changes never copy the body.
internal sealed class SvgTemplate
{
    private static readonly ConditionalWeakTable<string, SvgTemplate> _cache = new();
    private readonly KeyValuePair<string, object>[] _attributes;
    private readonly string _body;

    private SvgTemplate(string svg)
    {
        var root = XElement.Parse(svg, LoadOptions.PreserveWhitespace);
        if (root.Name.LocalName != "svg")
            throw new ArgumentException("An icon must have an SVG root.", nameof(svg));
        _attributes = root.Attributes().Select(a => new KeyValuePair<string, object>(
            a.IsNamespaceDeclaration && a.Name.LocalName == "xmlns" ? "xmlns" : a.Name.ToString(), a.Value)).ToArray();
        _body = string.Concat(root.Nodes().Select(n => n.ToString(SaveOptions.DisableFormatting)));
    }

    internal static SvgTemplate Get(string svg) => _cache.GetValue(svg, static raw => new SvgTemplate(raw));

    internal void Render(RenderTreeBuilder builder, Dictionary<string, object> attributes)
    {
        builder.OpenElement(0, "svg");
        builder.AddMultipleAttributes(1, _attributes);
        foreach (var (name, value) in attributes)
        {
            if (!IsSafeAttribute(name) || value is null)
                continue;
            if (name.Equals("hidden", StringComparison.OrdinalIgnoreCase))
            {
                if (value is true || value is "true" or "hidden") builder.AddAttribute(2, "hidden", true);
                continue;
            }
            if (value is string text)
            {
                if (text.Length != 0) builder.AddAttribute(2, name, text);
            }
            else if (value is bool flag)
                builder.AddAttribute(2, name, flag);
            else
                builder.AddAttribute(2, name, value is IFormattable fmt ? fmt.ToString(null, CultureInfo.InvariantCulture) : value.ToString());
        }
        builder.AddMarkupContent(3, _body);
        builder.CloseElement();
    }

    private static bool IsSafeAttribute(string key) =>
        key.StartsWith("aria-", StringComparison.OrdinalIgnoreCase) || key.StartsWith("data-", StringComparison.OrdinalIgnoreCase) ||
        key.Equals("class", StringComparison.OrdinalIgnoreCase) || key.Equals("style", StringComparison.OrdinalIgnoreCase) ||
        key.Equals("hidden", StringComparison.OrdinalIgnoreCase) || key.Equals("id", StringComparison.OrdinalIgnoreCase) ||
        key.Equals("title", StringComparison.OrdinalIgnoreCase) || key.Equals("role", StringComparison.OrdinalIgnoreCase) ||
        key.Equals("tabindex", StringComparison.OrdinalIgnoreCase) || key.Equals("focusable", StringComparison.OrdinalIgnoreCase);
}
