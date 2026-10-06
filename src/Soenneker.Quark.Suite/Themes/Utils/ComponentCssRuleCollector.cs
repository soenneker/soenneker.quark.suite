using System;
using System.Collections.Generic;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

internal struct ComponentCssRuleCollector
{
    private string? _firstSelector;
    private PooledCssDeclarations _firstDeclarations;
    private OrderedDictionary<string, PooledCssDeclarations>? _blocks;

    internal void Add(string selector, string declaration) => Add(selector, new ComponentCssDeclaration(null, declaration.AsMemory()));

    internal void AddStyle(string selector, string style)
    {
        var span = style.AsSpan();
        foreach (var range in span.Split(';'))
        {
            var segment = span[range];
            var trimmedStart = segment.TrimStart();
            var trimmed = trimmedStart.TrimEnd();
            if (!trimmed.IsEmpty)
                Add(selector, new ComponentCssDeclaration(null, style.AsMemory(range.Start.Value + segment.Length - trimmedStart.Length, trimmed.Length)));
        }
    }

    internal void Add(string selector, ComponentCssDeclaration declaration)
    {
        if (selector.IsNullOrWhiteSpace() || declaration.Property is null && declaration.Value.Span.IsWhiteSpace())
            return;

        _firstSelector ??= selector;
        if (string.Equals(_firstSelector, selector, StringComparison.Ordinal))
            _firstDeclarations.Add(declaration);
        else
        {
            _blocks ??= new OrderedDictionary<string, PooledCssDeclarations>(4, StringComparer.Ordinal);
            if (_blocks.TryGetValue(selector, out var declarations, out var index))
            {
                declarations.Add(declaration);
                _blocks.SetAt(index, declarations);
            }
            else
                _blocks.Add(selector, new PooledCssDeclarations(declaration));
        }
    }

    internal void AppendTo(ref PooledStringBuilder builder)
    {
        if (_firstSelector is null)
            return;

        if (builder.Length > 0)
            builder.Append('\n');

        AppendBlock(ref builder, _firstSelector, _firstDeclarations);
        if (_blocks is not null)
        {
            foreach (var block in _blocks)
            {
                builder.Append('\n');
                AppendBlock(ref builder, block.Key, block.Value);
            }
        }
    }

    internal void Dispose()
    {
        _firstDeclarations.Dispose();
        if (_blocks is not null)
        {
            foreach (var block in _blocks)
            {
                var declarations = block.Value;
                declarations.Dispose();
            }
        }

        this = default;
    }

    private static void AppendBlock(ref PooledStringBuilder builder, string selector, PooledCssDeclarations declarations)
    {
        builder.Append(selector);
        builder.Append(" {\n");
        for (var i = 0; i < declarations.Count; i++)
        {
            builder.Append("  ");
            var declaration = declarations[i];
            var value = declaration.Value.Span.TrimEnd("; ");
            if (declaration.Property is not null)
            {
                builder.Append(declaration.Property);
                builder.Append(value.IsEmpty ? ":" : ": ");
            }
            builder.Append(value);
            builder.Append(";\n");
        }
        builder.Append("}");
    }
}
