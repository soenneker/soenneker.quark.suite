using System;
using System.Collections.Generic;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

internal struct ComponentCssRuleCollector
{
    private string? _firstSelector;
    private PooledCssDeclarations _firstDeclarations;
    private OrderedDictionary<string, PooledCssDeclarations>? _blocks;

    internal void Add(string selector, ThemeCssDeclaration declaration)
    {
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
            builder.Append(declaration.Property);
            builder.Append(": ");
            builder.Append(declaration.Value);
            builder.Append(";\n");
        }
        builder.Append("}");
    }
}
