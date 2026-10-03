using System.Collections.Generic;

namespace Soenneker.Quark;

internal struct SmallBatch<T>
{
    private T? _first;
    private T? _second;
    private List<T>? _rest;
    internal int Count { get; private set; }
    internal readonly T this[int index] => index == 0 ? _first! : index == 1 ? _second! : _rest![index - 2];

    internal SmallBatch(T first)
    {
        _first = first;
        Count = 1;
    }

    internal SmallBatch(T first, T second)
    {
        _first = first;
        _second = second;
        Count = 2;
    }

    internal void Add(T value)
    {
        if (Count == 0) _first = value;
        else if (Count == 1) _second = value;
        else (_rest ??= []).Add(value);
        Count++;
    }
}
