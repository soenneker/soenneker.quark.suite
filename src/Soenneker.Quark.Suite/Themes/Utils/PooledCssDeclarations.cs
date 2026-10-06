using System;
using System.Buffers;

namespace Soenneker.Quark;

// The collector owns this buffer. Copies must never be disposed independently.
internal struct PooledCssDeclarations
{
    private ThemeCssDeclaration _first;
    private ThemeCssDeclaration _second;
    private ThemeCssDeclaration[]? _rest;
    internal int Count { get; private set; }

    internal readonly ThemeCssDeclaration this[int index] =>
        index == 0 ? _first : index == 1 ? _second : _rest![index - 2];

    internal PooledCssDeclarations(ThemeCssDeclaration first)
    {
        _first = first;
        Count = 1;
    }

    internal void Add(ThemeCssDeclaration value)
    {
        if (Count == 0)
            _first = value;
        else if (Count == 1)
            _second = value;
        else
        {
            var index = Count - 2;
            if (_rest is null)
                _rest = ArrayPool<ThemeCssDeclaration>.Shared.Rent(16);
            else if (index == _rest.Length)
            {
                var expanded = ArrayPool<ThemeCssDeclaration>.Shared.Rent(checked(_rest.Length * 2));
                _rest.CopyTo(expanded, 0);
                ArrayPool<ThemeCssDeclaration>.Shared.Return(_rest, clearArray: true);
                _rest = expanded;
            }

            _rest[index] = value;
        }

        Count++;
    }

    internal void Dispose()
    {
        if (_rest is not null)
            ArrayPool<ThemeCssDeclaration>.Shared.Return(_rest, clearArray: true);
        this = default;
    }
}
